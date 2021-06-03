using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cosmetic.Models;
using EC.SecurityService.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Twilio;
using Twilio.Rest.Api.V2010;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Monitor.V1;

namespace EC.SecurityService.Services
{

    public class Authy : IAuthy
    {
        private readonly IConfiguration Configuration;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<Authy> logger;
        private HttpClient client;
        private HttpClient clientAuthyVerifyConfig;

        private string authyBaseAddress = string.Empty;
        private string authyApiKey = string.Empty;
        private string twilioAccountSID = string.Empty;
        private string authyUserAgent = string.Empty;
        private string authyRequestContentType = "application/json";
        public string message { get; private set; }

        public Authy(IConfiguration config, IHttpClientFactory clientFactory,ILoggerFactory loggerFactory)
        {
            Configuration = config;
            logger = loggerFactory.CreateLogger<Authy>();

            authyBaseAddress = Configuration["SecurityServiceConfigs:AuthyBaseAddress"];
            authyApiKey = Configuration["SecurityServiceConfigs:AuthyApiKey"];
            authyUserAgent = Configuration["SecurityServiceConfigs:AuthyUserAgent"];            
            twilioAccountSID = Configuration["SecurityServiceConfigs:twilioAccountSID"];

            _clientFactory = clientFactory;
            client = this.CreateHttpClient();
            client.BaseAddress = new Uri(authyBaseAddress);
            client.DefaultRequestHeaders.Add("X-Authy-API-Key", authyApiKey);
            clientAuthyVerifyConfig = CreateHttpClient();
        }

        private HttpClient CreateHttpClient()
        {
            var clientAuthy = _clientFactory.CreateClient();
            clientAuthy.BaseAddress = new Uri(authyBaseAddress);
            clientAuthy.DefaultRequestHeaders.Add("Accept", authyRequestContentType);
            clientAuthy.DefaultRequestHeaders.Add("user-agent", authyUserAgent);        
            return clientAuthy;
        }

        public async Task<TokenVerificationResult> VerifyTokenAsync(string authyId, string token)
        {
            var result = await client.GetAsync($"/protected/json/verify/{token}/{authyId}");
            logger.LogDebug(result.ToString());
            logger.LogDebug(result.Content.ReadAsStringAsync().Result);
            var message = await result.Content.ReadAsStringAsync();

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return new TokenVerificationResult(message);
            }

            return new TokenVerificationResult(message, false);
        }

        public async Task<TokenVerificationResult> VerifyPhoneTokenAsync(string phoneNumber, string countryCode, string token)
        {
            var result = await client.GetAsync(
                $"/protected/json/phones/verification/check?phone_number={phoneNumber}&country_code={countryCode}&verification_code={token}"
            );

            logger.LogDebug(result.ToString());
            logger.LogDebug(result.Content.ReadAsStringAsync().Result);

            var message = await result.Content.ReadAsStringAsync();

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return new TokenVerificationResult(message);
            }

            return new TokenVerificationResult(message, false);
        }

        public async Task<HttpResponseMessage> SendSmsAsync(string authyId)
        {
            var result = await client.GetAsync($"/protected/json/sms/{authyId}?force=true");

            logger.LogDebug(result.ToString());

            //result.EnsureSuccessStatusCode();

            return result;//await result.Content.ReadAsStringAsync();
        }

        public async Task<string> PhoneVerificationCallRequestAsync(string countryCode, string phoneNumber)
        {
            var result = await client.PostAsync(
                $"/protected/json/phones/verification/start?via=call&country_code={countryCode}&phone_number={phoneNumber}",
                null
            );

            var content = await result.Content.ReadAsStringAsync();

            logger.LogDebug(result.ToString());
            logger.LogDebug(content);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> PhoneVerificationRequestAsync(string countryCode, string phoneNumber)
        {
            var result = await client.PostAsync(
                $"/protected/json/phones/verification/start?via=sms&country_code={countryCode}&phone_number={phoneNumber}",
                null
            );

            var content = await result.Content.ReadAsStringAsync();

            logger.LogDebug(result.ToString());
            logger.LogDebug(content);

            return result;
        }

        public async Task<string> CreateApprovalRequestAsync(string authyId, string seconds_to_expire)
        {
            var requestData = new Dictionary<string, string>() {
                { "message", "OneTouch Approval Request" },
                { "details", "My Message Details" },
                { "seconds_to_expire", seconds_to_expire }
            };

            var result = await client.PostAsJsonAsync(
                $"/onetouch/json/users/{authyId}/approval_requests",
                requestData
            );

            logger.LogDebug(result.ToString());
            var str_content = await result.Content.ReadAsStringAsync();
            logger.LogDebug(str_content);

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsAsync<Dictionary<string, object>>();
            var approval_request_data = (JObject)content["approval_request"];

            return (string)approval_request_data["uuid"];
        }

        public async Task<HttpResponseMessage> CheckRequestStatusAsync(string onetouch_uuid)
        {
            var result = await client.GetAsync($"/onetouch/json/approval_requests/{onetouch_uuid}");
            logger.LogDebug(result.ToString());
            var str_content = await result.Content.ReadAsStringAsync();
            logger.LogDebug(str_content);

            return result;
        }

        public async Task<object> VerifyAuthyConfigAsync()
        {
            var result = await client.GetAsync($"/protected/json/reporting/events?query[objects.phone_verification.s_via][gt]=sms");
            return result;
        }

        public async Task<object> VerifyAuthyApiKeyConfigAsync(string authyApiKeyStr)
        {
            try
            {

                if (clientAuthyVerifyConfig == null)
                {
                    clientAuthyVerifyConfig = this.CreateHttpClient();
                }
                if (clientAuthyVerifyConfig.DefaultRequestHeaders.Contains("X-Authy-API-Key"))
                {
                    clientAuthyVerifyConfig.DefaultRequestHeaders.Remove("X-Authy-API-Key");
                }
                clientAuthyVerifyConfig.DefaultRequestHeaders.Add("X-Authy-API-Key", authyApiKeyStr);
                var result = await clientAuthyVerifyConfig.GetAsync($"/protected/json/app/details");

                var contents = result.Content.ReadAsStringAsync().Result;

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    AuthyApplications resultContent = JsonConvert.DeserializeObject<AuthyApplications>(contents);
                    if (resultContent.app.app_id == 0 || resultContent.app.sms_enabled == false)
                    {
                        return new TokenVerificationResult("Validate Authy API Key is sucessfull.", false);
                    }
                    else
                    {
                        return new TokenVerificationResult("Validate Authy API Key is sucessfull.", true);
                    }
                }
                else
                {
                    return new TokenVerificationResult("Validate Authy API Key is sucessfull.", false);
                }
            }
            catch (Exception ex)
            {
                return new TokenVerificationResult("Validate Authy API Key is unsucessfull. Error " + ex.Message, false);
            }
        }
        public AccountResource VerifyTwilioConfig(string accountSid, string authToken)
        {
            try
            {
                TwilioClient.Init(accountSid, authToken);
                var account = AccountResource.Create();
                return account;
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                return null;
            }
        }

        public async Task<string> RegisterUserAsync(UserModel user)
        {
            var userRegData = new Dictionary<string, string>() {
                { "email", user.Email },
                { "country_code", user.CountryCode },
                { "cellphone", user.PhoneNumber }
            };
            var userRegRequestData = new Dictionary<string, object>() { };

            userRegRequestData.Add("user", userRegData);

            var encodedContent = new FormUrlEncodedContent(userRegData);

            var result = await client.PostAsJsonAsync("/protected/json/users/new", userRegRequestData);

            logger.LogDebug(result.Content.ReadAsStringAsync().Result);
            if (result.StatusCode == HttpStatusCode.OK)
            {

                result.EnsureSuccessStatusCode();

                var response = await result.Content.ReadAsAsync<Dictionary<string, object>>();

                return JObject.FromObject(response["user"])["id"].ToString();
            }
            return null;
        }
    }
}