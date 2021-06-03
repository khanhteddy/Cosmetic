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
    public class HoaDonsController : Controller
    {
        private readonly MyPhamContext _context;

        public HoaDonsController(MyPhamContext context)
        {
            _context = context;
        }

        // GET: HoaDons
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var myPhamContext = _context.HoaDon.Include(h => h.MaKhNavigation).Include(h => h.MaNvNavigation).Include(h => h.MaTrangThaiNavigation);
                return View(await myPhamContext.ToListAsync());
            }
        }

        // GET: HoaDons/Details/5
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

                var hoaDon = await _context.HoaDon
                    .Include(h => h.MaKhNavigation)
                    .Include(h => h.MaNvNavigation)
                    .Include(h => h.MaTrangThaiNavigation)
                    .FirstOrDefaultAsync(m => m.MaHd == id);
                if (hoaDon == null)
                {
                    return NotFound();
                }

                return View(hoaDon);
            }
        }

        // GET: HoaDons/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                ViewData["MaKh"] = new SelectList(_context.KhachHang, "MaKh", "MaKh");
                ViewData["MaNv"] = new SelectList(_context.NhanVien, "MaNv", "MaNv");
                ViewData["MaTrangThai"] = new SelectList(_context.TrangThai, "MaTrangThai", "MaTrangThai");
                return View();
            }
        }

        // POST: HoaDons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHd,MaKh,NgayDat,NgayGiao,MaNv,HoTen,DiaChi,CachThanhToan,CachVanChuyen,PhiVanChuyen,MaTrangThai,GhiChu,DienThoai,TenNgNhan,DtngNhan,DiaChiNgNhan")] HoaDon hoaDon)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(hoaDon);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["MaKh"] = new SelectList(_context.KhachHang, "MaKh", "MaKh", hoaDon.MaKh);
                ViewData["MaNv"] = new SelectList(_context.NhanVien, "MaNv", "MaNv", hoaDon.MaNv);
                ViewData["MaTrangThai"] = new SelectList(_context.TrangThai, "MaTrangThai", "MaTrangThai", hoaDon.MaTrangThai);
                return View(hoaDon);
            }
        }

        // GET: HoaDons/Edit/5
        [Route("[controller]/[action]")]
        public  IActionResult Edit(int? id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var hoaDon = _context.HoaDon.SingleOrDefault(hd => hd.MaHd == id);
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                if (hoaDon.DiaChi == null)
                {

                    return RedirectToAction("Index");
                }

                if (hoaDon == null)
                {
                    return RedirectToAction("Index");
                }
                ViewData["MaKh"] = new SelectList(_context.KhachHang, "MaKh", "MaKh", hoaDon.MaKh);
                ViewData["MaNv"] = new SelectList(_context.NhanVien, "MaNv", "MaNv", hoaDon.MaNv);
                ViewData["MaTrangThai"] = new SelectList(_context.TrangThai, "MaTrangThai", "MaTrangThai", hoaDon.MaTrangThai);
                return View(hoaDon);
            }
        }

        [Route("[controller]/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HoaDon hoaDon)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                


                if (ModelState.IsValid)
                {
                    if (hoaDon.DiaChi == null)
                    {
                        hoaDon.DiaChi = hoaDon.DiaChiNgNhan;
                        _context.Update(hoaDon);
                        await _context.SaveChangesAsync();
                    }
                    _context.Update(hoaDon);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["MaKh"] = new SelectList(_context.KhachHang, "MaKh", "MaKh", hoaDon.MaKh);
                ViewData["MaNv"] = new SelectList(_context.NhanVien, "MaNv", "MaNv", hoaDon.MaNv);
                ViewData["MaTrangThai"] = new SelectList(_context.TrangThai, "MaTrangThai", "MaTrangThai", hoaDon.MaTrangThai);
                return View(hoaDon);
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
                var hoaDon = await _context.HoaDon.FindAsync(id);
                var chitietHd = _context.ChiTietHd.Where(m => m.MaHd == id);
                foreach (var ct in chitietHd)
                {
                    _context.ChiTietHd.Remove(ct);
                }
                _context.HoaDon.Remove(hoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool HoaDonExists(int id)
        {
            return _context.HoaDon.Any(e => e.MaHd == id);
        }
    }
}
