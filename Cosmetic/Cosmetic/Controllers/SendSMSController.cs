using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EC.SecurityService.Common;
using EC.SecurityService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cosmetic.Models;
using Newtonsoft.Json;
using Cosmetic.Services;
using Twilio.Rest.Api.V2010.Account;

namespace Cosmetic.Controllers
{
    public class SendSMSController : Controller
    {
        private readonly IAuthy _authy;
        private readonly ISmsService _smsService;
        public readonly MyPhamContext _context;
        public SendSMSController(IAuthy authy, MyPhamContext context, ISmsService smsService)
        {
            _authy = authy;
            _context = context;
            _smsService = smsService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> RegisterUser(string phone)
        {
            UserModel userModel = new UserModel
            {
                Email = "agilehcmue@gmail.com",
                CountryCode = "+84",
                PhoneNumber = phone
            };

            var authyId = await _authy.RegisterUserAsync(userModel).ConfigureAwait(false);

            if (string.IsNullOrEmpty(authyId))
            {                
                return Json(new { success = false });
            }
            else
            {
                //update authyId in database
                KhachHang khachHang = _context.KhachHang.SingleOrDefault(kh => kh.PhoneNumber == phone);

                if(khachHang != null)
                {
                    khachHang.AuthyId = authyId;
                    _context.SaveChanges();
                }

                return Json(new { success = true, authyId = authyId });
            }
        }

        public async Task<IActionResult> Send(string phone)
        {
            //Phone ==> read DB to indicate AuthyId
            KhachHang khachHang = _context.KhachHang.SingleOrDefault(kh => kh.PhoneNumber == phone);

            if (khachHang != null && !string.IsNullOrEmpty(khachHang.AuthyId))
            {
                var sendSMSResponse = await _authy.SendSmsAsync(khachHang.AuthyId).ConfigureAwait(false);

                if (sendSMSResponse.StatusCode == HttpStatusCode.OK)
                {
                    var smsVerificationSucceedObject = JsonConvert.DeserializeObject<AccessCodeVerifyResult>(await sendSMSResponse.Content.ReadAsStringAsync());
                    if (smsVerificationSucceedObject.Success)
                    {
                        //Send SMS success
                        return Content($"G???i token th??nh c??ng t???i {phone}");
                    }
                    else
                    {
                        //Fail
                        return Content($"C?? l???i g???i tin nh???n t???i {phone}");
                    }
                }
            }

            return Content($"Kh??ng c?? kh??ch h??ng n??o c?? ??i???n tho???i: {phone}");
        }

        public async Task<IActionResult> VerifyToken(string phone, string token)
        {
            KhachHang khachHang = _context.KhachHang.SingleOrDefault(kh => kh.PhoneNumber == phone);

            if (khachHang != null && !string.IsNullOrEmpty(khachHang.AuthyId))
            {
                var validationResult = await _authy.VerifyTokenAsync(khachHang.AuthyId, token).ConfigureAwait(false);

                if (validationResult.Succeeded)
                {
                    khachHang.PhoneNumberConfirmed = true;
                    _context.SaveChanges();

                    return Json(new
                    {
                        Success = true,
                        Message = $"Your mobile phone {phone} verify successfully."
                    });
                }
            }
            return Content($"Kh??ng c?? kh??ch h??ng n??o c?? ??i???n tho???i: {phone}");
        }

        public async Task<IActionResult> SendSms(string phone, string content)
        {
            SmsMessage model = new SmsMessage
            {
                NameTo = "MoccaX1",
                NumberFrom = "+84352326234",
                NumberTo = "+84914136643",
                Body = "This is SMS from EC HCMUE",
                Greeting = "HCMUEr",
                Signature = "HIENLTH"
            };

        var result = await _smsService.Send(model);

        return Json(result);
        }
    }
} 