using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmetic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetic.Controllers
{
    public class AdminController : Controller
    {
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            return View();
        }
        private readonly MyPhamContext db;
        public AdminController(MyPhamContext context)
        {
            db = context;
        }
        [Route("[controller]/[action]")]
        public IActionResult Login()
        {
            return View();
        }
        [Route("[controller]/[action]")]
        [HttpPost]
        public IActionResult Login(AdminLogin model)
        {
            if (ModelState.IsValid)
            {
                NhanVien nv = db.NhanVien.SingleOrDefault(p => p.MaNv == model.MaNv && p.MatKhau == model.MatKhau);
                if (nv == null)
                {
                    ModelState.AddModelError("Loi", "Tài khoản hoặc mật khẩu không đúng");
                    return View();
                }
                //ghi session
                //HttpContext.Session.SetString("MaKH", kh.MaKh);
                HttpContext.Session.Set("MaNv", nv);
                //chuyển tới trang HangHoa (--> MyProfile)
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
        [Route("[controller]/[action]")]
        public IActionResult Logout()
        {
            //xóa session
            HttpContext.Session.Remove("MaNv");
            return RedirectToAction("Login", "Admin");
        }
    }
}