using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cosmetic.Models;

namespace Cosmetic.Controllers
{
    public class KhoHangsController : Controller
    {
        private readonly MyPhamContext _context;

        public KhoHangsController(MyPhamContext context)
        {
            _context = context;
        }

        // GET: KhoHangs
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var myPhamContext = _context.KhoHang.Include(k => k.MaSpNavigation);
                return View(await myPhamContext.ToListAsync());
            }
        }

        // GET: KhoHangs/Details/5
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Details(int? id)
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

                var khoHang = await _context.KhoHang
                    .Include(k => k.MaSpNavigation)
                    .FirstOrDefaultAsync(m => m.MaKho == id);
                if (khoHang == null)
                {
                    return NotFound();
                }

                return View(khoHang);
            }
        }

        // GET: KhoHangs/Create
        [Route("[controller]/[action]")]
        public IActionResult Create()
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                ViewData["MaSp"] = new SelectList(_context.SanPham, "MaSp", "MaSp");
                return View();
            }
        }

        // POST: KhoHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("[controller]/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKho,MaSp,SoLuong")] KhoHang khoHang)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(khoHang);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["MaSp"] = new SelectList(_context.SanPham, "MaSp", "MaSp", khoHang.MaSp);
                return View(khoHang);
            }
        }

        // GET: KhoHangs/Edit/5
        [Route("[controller]/[action]")]
        public IActionResult Edit(int? id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var khoHang = _context.KhoHang.SingleOrDefault(k => k.MaKho== id);
                if (id == null)
                {
                    return NotFound();
                }

                if (khoHang == null)
                {
                    return NotFound();
                }
                ViewData["MaSp"] = new SelectList(_context.SanPham, "MaSp", "MaSp", khoHang.MaSp);
                return View(khoHang);
            }
        }

        // POST: KhoHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("[controller]/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( KhoHang khoHang)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                

                if (ModelState.IsValid)
                {
                   
                        _context.Update(khoHang);
                        await _context.SaveChangesAsync();   
                    return RedirectToAction(nameof(Index));
                }
                ViewData["MaSp"] = new SelectList(_context.SanPham, "MaSp", "MaSp", khoHang.MaSp);
                return View(khoHang);
            }
        }

       
        // POST: KhoHangs/Delete/5
        [Route("[controller]/[action]")]
      
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var khoHang = await _context.KhoHang.FindAsync(id);
                _context.KhoHang.Remove(khoHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool KhoHangExists(int id)
        {
            return _context.KhoHang.Any(e => e.MaKho == id);
        }
    }
}
