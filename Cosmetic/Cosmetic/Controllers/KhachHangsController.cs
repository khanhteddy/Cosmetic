using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cosmetic.Models;
using Cosmetic.Encrytions;

namespace WebOnline.Controllers
{
    public class KhachHangsController : Controller
    {
        private readonly MyPhamContext _context;

        public KhachHangsController(MyPhamContext context)
        {
            _context = context;
        }

        // GET: KhachHangs
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                return View(await _context.KhachHang.ToListAsync());
            }
        }

        // GET: KhachHangs/Details/5
       [Route("[controller]/[action]")]
        public async Task<IActionResult> Details(string id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var khachHang = await _context.KhachHang
                    .FirstOrDefaultAsync(m => m.MaKh == id);
                if (khachHang == null)
                {
                    return NotFound();
                }


                return View(khachHang);
            }
        }

        // GET: KhachHangs/Create
        [Route("[controller]/[action]")]
        public IActionResult Create()
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                return View();
            }
        }

        // POST: KhachHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("[controller]/[action]")]
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKh,MatKhau,HoTen,GioiTinh,NgaySinh,DiaChi,DienThoai,Email,HieuLuc,VaiTro,RandomKey")] KhachHang khachHang)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    PasswordHasher passwordHasher = new PasswordHasher();
                    khachHang.MatKhau = passwordHasher.HashPassword(khachHang.MatKhau);
                    _context.Add(khachHang);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(khachHang);
            }
        }

        // GET: KhachHangs/Edit/5
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Edit(string id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var khachHang = await _context.KhachHang.FindAsync(id);
                if (khachHang == null)
                {
                    return NotFound();
                }
                return View(khachHang);
            }
        }

        // POST: KhachHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
       [Route("[controller]/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaKh,MatKhau,HoTen,GioiTinh,NgaySinh,DiaChi,DienThoai,Email,HieuLuc,VaiTro,RandomKey")] KhachHang khachHang)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                if (id != khachHang.MaKh)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        PasswordHasher passwordHasher = new PasswordHasher();
                        khachHang.MatKhau = passwordHasher.HashPassword(khachHang.MatKhau);
                        _context.Update(khachHang);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!KhachHangExists(khachHang.MaKh))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(khachHang);
            }
        }

        // GET: KhachHangs/Delete/5
       [Route("[controller]/[action]")]
        public async Task<IActionResult> Delete(string id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var khachHang = await _context.KhachHang
                    .FirstOrDefaultAsync(m => m.MaKh == id);
                if (khachHang == null)
                {
                    return NotFound();
                }

                return View(khachHang);
            }
        }

        // POST: KhachHangs/Delete/5
       [Route("[controller]/[action]")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var khachHang = await _context.KhachHang.FindAsync(id);
                _context.KhachHang.Remove(khachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool KhachHangExists(string id)
        {
            return _context.KhachHang.Any(e => e.MaKh == id);
        }
        [Route("[controller]/[action]")]
        public IActionResult AddnewKhachHang()
        {
            return View();
        }
        [Route("[controller]/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddnewKhachHang
([Bind("MaKh,MatKhau,HoTen,GioiTinh,NgaySinh,DiaChi,DienThoai,Email,HieuLuc,VaiTro,RandomKey")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                PasswordHasher passwordHasher = new PasswordHasher();
                khachHang.MatKhau = passwordHasher.HashPassword(khachHang.MatKhau);
                _context.Add(khachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction("../Home/Index");
            }
            return View(khachHang);
        }
    }
}
