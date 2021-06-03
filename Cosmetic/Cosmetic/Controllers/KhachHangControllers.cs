using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cosmetic.Models;
using Microsoft.AspNetCore.DataProtection;
using Cosmetic.Encrytions; // To use hash call namespace Cosmetic.Encrytions
using Newtonsoft.Json;
using EC.SecurityService.Common;
using Cosmetic.Services;
using EC.SecurityService.Services;

namespace Cosmetic.Controllers
{
    // user-defined exceptions
    public class UserDefException: Exception 
    {
        public UserDefException(string message): base(message){}
    }
    public class KhachHangController : Controller
    {
        //private string key = "Cyg-X1"; //key to encrypt and decrypt
        private readonly MyPhamContext db;
        private readonly IAuthy _authy;
        private readonly ISmsService _smsService;

        //Encrytion ecr = new Encrytion();
        public KhachHangController(MyPhamContext context, IDataProtectionProvider provider, IAuthy auth, ISmsService smsService)
        {
            db = context;
            _smsService = smsService;
            _authy = auth;
        }
        //[Route("[controller]/[action]")]
        public IActionResult Index()
        {
            /*KhachHang kh = HttpContext.Session.Get<KhachHang>("TaiKhoan");
            var query = from info in db.KhachHang
                            where info.MaKh == kh.MaKh
                            select info;
            foreach (KhachHang ds in query)
            {
                ViewBag.MaKh = ds.MaKh;
                ViewBag.HoTen = ecr.DecryptString(ds.HoTen, key);
                ViewBag.GioiTinh = ds.GioiTinh;
                ViewBag.DiaChi = ecr.DecryptString(ds.DiaChi, key);
                ViewBag.NgaySinh = ds.NgaySinh;
                ViewBag.DienThoai = ecr.DecryptString(ds.DienThoai, key);
                ViewBag.Email = ecr.DecryptString(ds.Email, key);
            }*/

            return View();
        }
        [Route("[controller]/[action]")]
        public IActionResult DoiMK()
        {
            KhachHang kh = HttpContext.Session.Get<KhachHang>("TaiKhoan");
            // Use hash
            PasswordHasher passwordHasher = new PasswordHasher(); 
                       
            string passold = HttpContext.Request.Form["nhapmkcu"].ToString();
            string pass1 = HttpContext.Request.Form["nhapmk"].ToString();
            string pass2 = HttpContext.Request.Form["nhaplaimk"].ToString();   
            //bool isPass = Regex.IsMatch(pass2, @"^((?!.*[\s])(?=.*[A-Z])(?=.*\d).{8,15})", RegexOptions.IgnoreCase);        
            
            try
            {
                if(pass1 != pass2 || 
                passwordHasher.VerifyHashedPassword(kh.MatKhau, passold) == PasswordVerificationResult.Failed)
                {
                    throw new UserDefException("Mật khẩu không khớp!");
                }
                else if (passwordHasher.VerifyHashedPassword(kh.MatKhau, passold) == PasswordVerificationResult.ErrorNull)
                {
                    throw new UserDefException("Mật khẩu không hợp lệ!");
                }
                else
                {
                    var query = from info in db.KhachHang
                                where info.MaKh == kh.MaKh
                                select info;

                    foreach (KhachHang ds in query)
                    {
                        if (passwordHasher.HashPassword(pass2) == "IVP")
                        {
                           throw new UserDefException("Mật khẩu không hợp lệ!");
                        }
                        else
                        {
                            ds.MatKhau = passwordHasher.HashPassword(pass2);
                            kh.MatKhau = ds.MatKhau;
                            ViewBag.Result = "Đã đổi mật khẩu thành công!";
                            HttpContext.Session.Set("TaiKhoan", kh);
                        }
                    }            
                    db.SaveChanges();
                    return View("Index");
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
            return View("Index");
        }
        [Route("[controller]/[action]")]
        public async Task<IActionResult> DoiTT()
        {
            string hoten = HttpContext.Request.Form["hoten"].ToString();
            string gioi = HttpContext.Request.Form["gioi"].ToString();
            string diachi = HttpContext.Request.Form["diachi"].ToString();    
            string ngaysinh = HttpContext.Request.Form["ngaysinh"].ToString();
            string sdt = HttpContext.Request.Form["sdt"].ToString();
            //Check number is valid in Vietnam (Ex: 0977666333 is valid, 01234567899 is not valid)
            bool isSdt = Regex.IsMatch(sdt, @"(3\d{8}|5\d{8}|7\d{8}|8\d{8}|9\d{8})", RegexOptions.IgnoreCase);
            string email = HttpContext.Request.Form["email"].ToString();
            //Check email is valid?
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            bool check_email_changed = false;
            
            try
            {
                KhachHang kh = HttpContext.Session.Get<KhachHang>("TaiKhoan");
                Encrytion ecr = new Encrytion();

                var query = from info in db.KhachHang
                            where info.MaKh == kh.MaKh
                            select info;
                foreach (KhachHang ds in query)
                {
                    //ds.HoTen = ecr.EncryptString(hoten, key);
                    ds.HoTen = hoten;
                    kh.HoTen = ds.HoTen;
                    if (gioi == "False")
                    {                    
                        ds.GioiTinh = false;
                        kh.GioiTinh = ds.GioiTinh;
                    }
                    else
                    {
                        ds.GioiTinh = true;
                        kh.GioiTinh = ds.GioiTinh;
                    }
                    //ds.DiaChi = ecr.EncryptString(diachi, key);
                    ds.DiaChi = diachi;
                    kh.DiaChi = ds.DiaChi;
                    ds.NgaySinh = Convert.ToDateTime(ngaysinh);
                    kh.NgaySinh = ds.NgaySinh;
                    if (isSdt)
                    {
                        //ds.DienThoai = ecr.EncryptString(sdt, key);
                        ds.DienThoai = sdt;
                        kh.DienThoai = ds.DienThoai;
                    }
                    else
                    {
                        throw (new UserDefException("Số điện thoại không hợp lệ!"));
                    }
                    if (isEmail)
                    {
                        if (email != kh.Email)
                            check_email_changed = true;
                        //ds.Email = ecr.EncryptString(email, key);
                        ds.Email = email;
                        kh.Email = ds.Email;
                    }
                    else
                    {
                        throw new UserDefException("Email không hợp lệ!");
                    }
                }

                if (check_email_changed)
                {
                    UserModel userModel = new UserModel
                        {
                            Email = kh.Email,
                            CountryCode = "+84",
                            PhoneNumber = kh.DienThoai
                        };

                        var authyId = await _authy.RegisterUserAsync(userModel).ConfigureAwait(false);

                        if (string.IsNullOrEmpty(authyId))
                        {                
                            //return Json(new { success = false });
                            throw new UserDefException("Số điện thoại chưa chuẩn?");
                        }
                        else
                        {
                            //update authyId in database
                            //khachHang = db.KhachHang.SingleOrDefault(kh => kh.PhoneNumber == phonenum);

                            if(kh != null)
                            {
                                kh.AuthyId = authyId;
                                /*kh.PhoneNumberConfirmed = false;
                                db.Add(kh);         
                                await db.SaveChangesAsync();*/
                            }

                            //return Json(new { success = true, authyId = authyId });
                        }
                        SmsMessage model = new SmsMessage
                        {
                            NameTo = kh.HoTen,
                            NumberFrom = "+12055649222",
                            NumberTo = "+84" + kh.DienThoai,
                            Body = "Bạn đã thay đổi email lúc" + DateTime.Now.ToString() +". Nếu có vấn đề vui lòng liên hệ Admin.",
                            Greeting = "Thanh",
                            Signature = "Cosmetic Project"
                        };

                        await _smsService.Send(model);
                }
                HttpContext.Session.Set("TaiKhoan", kh);
                ViewBag.Result2 = "Đã cập nhật thông tin thành công!";
                db.SaveChanges();
                return View("Index");
            }
            catch (UserDefException e)
            {
                ViewBag.Result2 = e.Message;
            }
            catch (Exception e)
            {
                ViewBag.Result2 = e.Message;
            }
            return View("Index");
        }

    }
}
