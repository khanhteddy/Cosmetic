using System.Net.Http;
using System.Threading.Tasks;
using Cosmetic.Models;
using EC.SecurityService.Common;
using Twilio.Rest.Api.V2010;

namespace EC.SecurityService.Services
{
    public interface IAuthy
    {
        Task<TokenVerificationResult> VerifyTokenAsync(string authyId, string token);
        Task<TokenVerificationResult> VerifyPhoneTokenAsync(string phoneNumber, string countryCode, string token);
        Task<HttpResponseMessage> SendSmsAsync(string authyId);
        Task<string> PhoneVerificationCallRequestAsync(string countryCode, string phoneNumber);
        Task<HttpResponseMessage> PhoneVerificationRequestAsync(string countryCode, string phoneNumber);
        Task<string> CreateApprovalRequestAsync(string authyId, string seconds_to_expire);
        Task<HttpResponseMessage> CheckRequestStatusAsync(string onetouch_uuid);
        Task<object> VerifyAuthyConfigAsync();
        Task<string> RegisterUserAsync(UserModel user);
        AccountResource VerifyTwilioConfig(string accountSid, string authToken);
        Task<object> VerifyAuthyApiKeyConfigAsync(string authyApiKeyStr);
    }
}