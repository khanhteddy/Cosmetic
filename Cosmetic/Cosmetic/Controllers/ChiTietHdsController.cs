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
    public class ChiTietHdsController : Controller
    {
        private readonly MyPhamContext _context;

        public ChiTietHdsController(MyPhamContext context)
        {
            _context = context;
        }

        // GET: ChiTietHds
        public async Task<IActionResult> Index(int? id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var myPhamContext = _context.ChiTietHd.Include(c => c.MaHdNavigation).Include(c => c.MaSpNavigation).Where(m => m.MaHd == id);
                return View(await myPhamContext.ToListAsync());
            }
        }
        public async Task<IActionResult> ThongKe()
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var myPhamContext = _context.ChiTietHd.Include(c => c.MaHdNavigation).Include(c => c.MaSpNavigation);
                return View(await myPhamContext.ToListAsync());
            }
        }

        // GET: ChiTietHds/Details/5
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

                var chiTietHd = await _context.ChiTietHd
                    .Include(c => c.MaHdNavigation)
                    .Include(c => c.MaSpNavigation)
                    .FirstOrDefaultAsync(m => m.MaCt == id);
                if (chiTietHd == null)
                {
                    return NotFound();
                }

                return View(chiTietHd);
            }
        }

        // GET: ChiTietHds/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                ViewData["MaHd"] = new SelectList(_context.HoaDon, "MaHd", "DiaChi");
                ViewData["MaSp"] = new SelectList(_context.SanPham, "MaSp", "MaSp");
                return View();
            }
        }

        // POST: ChiTietHds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaCt,MaHd,MaSp,DonGia,SoLuong,GiamGia")] ChiTietHd chiTietHd)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(chiTietHd);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["MaHd"] = new SelectList(_context.HoaDon, "MaHd", "DiaChi", chiTietHd.MaHd);
                ViewData["MaSp"] = new SelectList(_context.SanPham, "MaSp", "MaSp", chiTietHd.MaSp);
                return View(chiTietHd);
            }
        }

        // GET: ChiTietHds/Edit/5
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

                var chiTietHd = await _context.ChiTietHd.FindAsync(id);
                if (chiTietHd == null)
                {
                    return NotFound();
                }
                ViewData["MaHd"] = new SelectList(_context.HoaDon, "MaHd", "DiaChi", chiTietHd.MaHd);
                ViewData["MaSp"] = new SelectList(_context.SanPham, "MaSp", "MaSp", chiTietHd.MaSp);
                return View(chiTietHd);
            }
        }

        // POST: ChiTietHds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaCt,MaHd,MaSp,DonGia,SoLuong,GiamGia")] ChiTietHd chiTietHd)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                if (id != chiTietHd.MaCt)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(chiTietHd);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ChiTietHdExists(chiTietHd.MaCt))
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
                ViewData["MaHd"] = new SelectList(_context.HoaDon, "MaHd", "DiaChi", chiTietHd.MaHd);
                ViewData["MaSp"] = new SelectList(_context.SanPham, "MaSp", "MaSp", chiTietHd.MaSp);
                return View(chiTietHd);
            }
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> Delete(int ?id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var chiTietHd = await _context.ChiTietHd.FindAsync(id);
                _context.ChiTietHd.Remove(chiTietHd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool ChiTietHdExists(int id)
        {
            return _context.ChiTietHd.Any(e => e.MaCt == id);
        }
    }
}
