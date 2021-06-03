using Cosmetic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.Extensions.Configuration;
using Twilio.Types;
using Twilio;

namespace Cosmetic.Services
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration Configuration;
        public SmsService(IConfiguration configuration)
        {
            Configuration = configuration;

            twilioApiToken = Configuration["SecurityServiceConfigs:TwilioApiTokenSandbox"];
            twilioAccountSID = Configuration["SecurityServiceConfigs:TwilioAccountSIDSandbox"];

            TwilioClient.Init(twilioAccountSID, twilioApiToken);
        }

        private string twilioAccountSID = string.Empty;
        private string twilioApiToken = string.Empty;

        public async Task<MessageResource> Send(SmsMessage smsMessage)
        {
            var messageBody = ConstructMessageBody(smsMessage);

            var result = await MessageResource.CreateAsync(
                        from: new PhoneNumber(smsMessage.NumberFrom),
                        to: new PhoneNumber(smsMessage.NumberTo),
                        body: messageBody);

            return result;
        }

        private string ConstructMessageBody(SmsMessage smsMessage)
        {
            return $"{smsMessage.Greeting} {smsMessage.NameTo}, {smsMessage.Body} {smsMessage.Signature}";
        }
    }
}