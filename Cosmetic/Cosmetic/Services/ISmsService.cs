using System.Threading.Tasks;
using Cosmetic.Models;
using Twilio.Rest.Api.V2010.Account;

namespace Cosmetic.Services
{
    public interface ISmsService
    {
        Task<MessageResource> Send(SmsMessage smsMessage);
    }
}