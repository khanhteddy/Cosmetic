using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmetic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Cosmetic.Encrytions;
using Newtonsoft.Json;
using EC.SecurityService.Common;
using Cosmetic.Services;
using EC.SecurityService.Services;

namespace Cosmetic.Controllers
{
    public class DangNhapController : Controller
    {
        private readonly IAuthy _authy;
        private readonly ISmsService _smsService;
        private readonly MyPhamContext db;
        private static string phonenum;
        //private string key = "Cyg-X1"; //key to encrypt and decrypt
        PasswordHasher passwordHasher = new PasswordHasher();
        //Encrytion ecr = new Encrytion(); // Encrypt HoTen, DiaChi, DienThoai, Email 
        public DangNhapController(MyPhamContext context, IAuthy auth, ISmsService smsService)
        {
            _authy = auth;
            db = context;
            _smsService = smsService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("[controller]/[action]")]
        //  public async Task<IActionResult> DangNhap(LoginViewModel model)
        public ActionResult DangNhap(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                KhachHang kh = db.KhachHang.SingleOrDefault(p => p.MaKh == model.MaKh && //p.MatKhau==model.MatKhau);//
               passwordHasher.VerifyHashedPassword(p.MatKhau, model.MatKhau) == PasswordVerificationResult.Success);
                if (kh == null)
                {
                    ModelState.AddModelError("Loi", "Thông tin tài khoản hoặc mật khẩu không hợp lệ.");
                    return View("Index");
                }
                else
                {
                    HttpContext.Session.Set("TaiKhoan", kh);
                    return RedirectToAction("Index", "Home");

                    #region try catch
                    #endregion
                }

            }
            return View("Index");
        }

        [Route("[controller]/[action]")]
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> DangKy([Bind("MaKh,MatKhau,HoTen,GioiTinh,NgaySinh,DiaChi,DienThoai,Email,HieuLuc,VaiTro,RandomKey")] KhachHang khachHang)
        {
            phonenum = khachHang.DienThoai;
            try
            {
                if (ModelState.IsValid)
                {
                    if (passwordHasher.HashPassword(khachHang.MatKhau) == "IVP")
                    {
                        throw new UserDefException("Mật khẩu đã đặt không hợp lệ!");
                    }
                    if (!Regex.IsMatch(khachHang.DienThoai, @"^((09(\d){8})|(086(\d){7})|(088(\d){7})|(089(\d){7})|(01(\d){9}))$" /*@"(3\d{8}|5\d{8}|7\d{8}|8\d{8}|9\d{8})"*/, RegexOptions.IgnoreCase))
                    {
                        throw new UserDefException("Số điện thoại không hợp lệ!");
                    }
                    if (!Regex.IsMatch(khachHang.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                    {
                        throw new UserDefException("Email không hợp lệ!");
                    }
                    else
                    {
                        khachHang.MatKhau = passwordHasher.HashPassword(khachHang.MatKhau);
                        /*khachHang.HoTen = ecr.EncryptString(khachHang.HoTen, key);
                        khachHang.DiaChi = ecr.EncryptString(khachHang.DiaChi, key);
                        khachHang.DienThoai = ecr.EncryptString(khachHang.DienThoai, key);
                        khachHang.Email = ecr.EncryptString(khachHang.Email, key);*/
                        khachHang.PhoneNumber = khachHang.DienThoai;

                        UserModel userModel = new UserModel
                        {
                            Email = khachHang.Email,
                            CountryCode = "+84",
                            PhoneNumber = phonenum
                        };

                        var authyId = await _authy.RegisterUserAsync(userModel).ConfigureAwait(false);

                        if (string.IsNullOrEmpty(authyId))
                        {
                            
                            throw new UserDefException("Số điện thoại chưa chuẩn?");
                        }
                        else
                        {
                            if (khachHang != null)
                            {
                                khachHang.AuthyId = authyId;
                                khachHang.PhoneNumberConfirmed = false;
                                db.Add(khachHang);
                                await db.SaveChangesAsync();
                            }

                            
                        }
                        

                        if (khachHang != null && !string.IsNullOrEmpty(khachHang.AuthyId))
                        {
                            var sendSMSResponse = await _authy.SendSmsAsync(khachHang.AuthyId).ConfigureAwait(false);

                            if (sendSMSResponse.StatusCode == HttpStatusCode.OK)
                            {
                                var smsVerificationSucceedObject = JsonConvert.DeserializeObject<AccessCodeVerifyResult>(await sendSMSResponse.Content.ReadAsStringAsync());
                                if (smsVerificationSucceedObject.Success)
                                {
                                    //Send SMS success
                                    return View("XacMinh");
                                    throw new UserDefException($"Gửi token thành công tới {phonenum}");

                                }
                                else
                                {
                                    //Fail
                                    throw new UserDefException($"Có lỗi gửi tin nhắn tới {phonenum}");
                                }
                            }
                        }
                        else
                            throw new UserDefException($"Không có khách hàng nào có điện thoại: {phonenum}");
                    }
                }
            }
            catch (UserDefException e)
            {
                ViewBag.Result = e.Message;
            }
            catch (Exception e)
            {
                ViewBag.Result = e.Message;
            }
            return View("DangKy");
        }
        [Route("[controller]/[action]")]
        public IActionResult DangXuat()
        {
            //xóa session
            HttpContext.Session.Remove("TaiKhoan");
            return RedirectToAction("Index", "Home");
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> XacMinh()
        {
            try
            {
                string token = HttpContext.Request.Form["token"].ToString();
                KhachHang khachHang = db.KhachHang.SingleOrDefault(kh => kh.PhoneNumber == phonenum);

                if (khachHang != null && !string.IsNullOrEmpty(khachHang.AuthyId))
                {
                    var validationResult = await _authy.VerifyTokenAsync(khachHang.AuthyId, token).ConfigureAwait(false);

                    if (validationResult.Succeeded)
                    {
                        khachHang.PhoneNumberConfirmed = true;
                       
                        
                        SmsMessage model = new SmsMessage
                        {
                            NameTo = khachHang.HoTen,
                            NumberFrom = "+84352326234",
                            NumberTo = "+84" + phonenum,
                            Body = "",
                            Greeting = "",
                            Signature = ""
                        };
                               db.SaveChanges();
                        await _smsService.Send(model);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Result =
                        $"Không thể xác minh +84{phonenum}. Vui lòng kiểm tra SĐT hoặc mã đã nhập có đúng không.";
                    }
                }
                else
                    throw new UserDefException($"Không có khách hàng nào có điện thoại: {phonenum}");
            }
            catch (UserDefException e)
            {
                ViewBag.Result = e.Message;
            }
            catch (Exception e)
            {
                ViewBag.Result = e.Message;
            }
            return View("XacMinh");
        }
        [Route("[controller]/[action]")]
        public async Task<IActionResult> XacMinhDangNhap()
        {
            try
            {
                string token = HttpContext.Request.Form["token"].ToString();
                KhachHang khachHang = db.KhachHang.SingleOrDefault(kh => kh.PhoneNumber == phonenum);
                if (khachHang != null && !string.IsNullOrEmpty(khachHang.AuthyId))
                {
                    var validationResult = await _authy.VerifyTokenAsync(khachHang.AuthyId, token).ConfigureAwait(false);
                    if (validationResult.Succeeded)
                    {
                        khachHang.PhoneNumberConfirmed = true;
                        db.SaveChanges();
                        HttpContext.Session.Set("TaiKhoan", khachHang);
                        /*return Json(new
                        {
                            Success = true,
                            Message = $"Số điện thoại của bạn {phonenum} đã xác minh thành công."
                        });*/
                        ViewBag.Result =
                        $"Số điện thoại của bạn +84{phonenum} đã xác minh thành công. Vui lòng chờ chuyển đến trang chủ...";
                        Response.Headers.Add("REFRESH", "5;URL=../Home");
                    }
                    else
                    {
                        ViewBag.Result =
                        $"Không thể xác minh +84{phonenum}. Vui lòng kiểm tra SĐT hoặc mã đã nhập có đúng không.";
                    }
                }
                else
                    throw new UserDefException($"Không có khách hàng nào có điện thoại: {phonenum}");
            }
            catch (UserDefException e)
            {
                ViewBag.Result = e.Message;
            }
            catch (Exception e)
            {
                ViewBag.Result = e.Message;
            }
            return View("XacMinhDangNhap");
        }
    }
}