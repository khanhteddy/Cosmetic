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
    public class BinhLuansController : Controller
    {
        private readonly MyPhamContext _context;

        public BinhLuansController(MyPhamContext context)
        {
            _context = context;
        }

        // GET: BinhLuans
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var myPhamContext = _context.BinhLuan.Include(b => b.MaKhNavigation).Include(b => b.MaSpNavigation);
                return View(await myPhamContext.ToListAsync());
            }
        }

        // GET: BinhLuans/Details/5
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

                var binhLuan = await _context.BinhLuan
                    .Include(b => b.MaKhNavigation)
                    .Include(b => b.MaSpNavigation)
                    .FirstOrDefaultAsync(m => m.MaBl == id);
                if (binhLuan == null)
                {
                    return NotFound();
                }

                return View(binhLuan);
            }
        }

        // GET: BinhLuans/Create
        [Route("[controller]/[action]")]
        public IActionResult Create()
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                ViewData["MaKh"] = new SelectList(_context.KhachHang, "MaKh", "MaKh");
                ViewData["MaSp"] = new SelectList(_context.SanPham, "MaSp", "MaSp");
                return View();
            }
        }

        // POST: BinhLuans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("[controller]/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBl,MaSp,MaKh,NgayBl,HoTen,Email,NoiDung")] BinhLuan binhLuan)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(binhLuan);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["MaKh"] = new SelectList(_context.KhachHang, "MaKh", "MaKh", binhLuan.MaKh);
                ViewData["MaSp"] = new SelectList(_context.SanPham, "MaSp", "MaSp", binhLuan.MaSp);
                return View(binhLuan);
            }
        }

        // GET: BinhLuans/Edit/5
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Edit(int? id)
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

                var binhLuan = await _context.BinhLuan.FindAsync(id);
                if (binhLuan == null)
                {
                    return NotFound();
                }
                ViewData["MaKh"] = new SelectList(_context.KhachHang, "MaKh", "MaKh", binhLuan.MaKh);
                ViewData["MaSp"] = new SelectList(_context.SanPham, "MaSp", "MaSp", binhLuan.MaSp);
                return View(binhLuan);
            }
        }

        // POST: BinhLuans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Edit(int id, [Bind("MaBl,MaSp,MaKh,NgayBl,HoTen,Email,NoiDung")] BinhLuan binhLuan)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                if (id != binhLuan.MaBl)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(binhLuan);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BinhLuanExists(binhLuan.MaBl))
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
                ViewData["MaKh"] = new SelectList(_context.KhachHang, "MaKh", "MaKh", binhLuan.MaKh);
                ViewData["MaSp"] = new SelectList(_context.SanPham, "MaSp", "MaSp", binhLuan.MaSp);
                return View(binhLuan);
            }
        }

        
        // POST: BinhLuans/Delete/5
        [Route("[controller]/[action]")]
        
        public async Task<IActionResult> Delete(int ?id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var binhLuan = await _context.BinhLuan.FindAsync(id);
                _context.BinhLuan.Remove(binhLuan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool BinhLuanExists(int id)
        {
            return _context.BinhLuan.Any(e => e.MaBl == id);
        }
    }
}
