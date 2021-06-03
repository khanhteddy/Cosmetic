using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cosmetic.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Cosmetic.Controllers
{
    public class SanPhamsController : Controller
    {
        private readonly MyPhamContext _context;

        public SanPhamsController(MyPhamContext context)
        {
            _context = context;
        }

        // GET: SanPhams
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var myPhamContext = _context.SanPham.Include(s => s.MaLoaiNavigation).Include(s => s.MaNccNavigation);
                return View(await myPhamContext.ToListAsync());
            }
        }

        // GET: SanPhams/Details/5
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

                var sanPham = await _context.SanPham
                    .Include(s => s.MaLoaiNavigation)
                    .Include(s => s.MaNccNavigation)
                    .FirstOrDefaultAsync(m => m.MaSp == id);
                if (sanPham == null)
                {
                    return NotFound();
                }

                return View(sanPham);
            }
        }

        // GET: SanPhams/Create
        [Route("[controller]/[action]")]
        public IActionResult Create()
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                ViewData["MaLoai"] = new SelectList(_context.Loai, "MaLoai", "MaLoai");
                ViewData["MaNcc"] = new SelectList(_context.NhaCungCap, "MaNcc", "MaNcc");
                return View();
            }
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("[controller]/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SanPham sanPham,IFormFile Hinh,IFormFile Hinh2)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (Hinh != null)
                    {
                        string urlFull = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh","beauty", Hinh.FileName);
                        using(var file= new FileStream(urlFull, FileMode.Create))
                        {
                            await Hinh.CopyToAsync(file);
                        }
                        sanPham.Hinh = Hinh.FileName;
                    }
                    if (Hinh2 != null)
                    {
                        string urlFull1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", "beauty", Hinh2.FileName);
                        using (var file = new FileStream(urlFull1, FileMode.Create))
                        {
                            await Hinh2.CopyToAsync(file);
                        }
                        sanPham.Hinh2 = Hinh2.FileName;
                    }
                    _context.Add(sanPham);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["MaLoai"] = new SelectList(_context.Loai, "MaLoai", "MaLoai", sanPham.MaLoai);
                ViewData["MaNcc"] = new SelectList(_context.NhaCungCap, "MaNcc", "MaNcc", sanPham.MaNcc);
                return View(sanPham);
            }
        }

        // GET: SanPhams/Edit/5
        [Route("[controller]/[action]")]
        public IActionResult Edit(int? id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var sanpham = _context.SanPham.SingleOrDefault(sp => sp.MaSp == id);
                if (id == null)
                {
                    return RedirectToAction("Index");
                }


                if (sanpham == null)
                {
                    return RedirectToAction("Index");
                }
                ViewData["MaLoai"] = new SelectList(_context.Loai, "MaLoai", "MaLoai", sanpham.MaLoai);
                ViewData["MaNcc"] = new SelectList(_context.NhaCungCap, "MaNcc", "MaNcc", sanpham.MaNcc);
                return View(sanpham);
            }
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("[controller]/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SanPham sanPham,IFormFile HinhUpdate,IFormFile Hinh2Update)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {

                if (ModelState.IsValid)
                {
                    if (HinhUpdate != null )
                    {
                        string urlFull = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh","beauty", HinhUpdate.FileName);
                        using (var file = new FileStream(urlFull, FileMode.Create))
                        {
                             HinhUpdate.CopyToAsync(file);
                        }
                        sanPham.Hinh = HinhUpdate.FileName;

                    }
                    if (Hinh2Update != null)
                    {
                        string urlFull1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", "beauty", Hinh2Update.FileName);
                        using (var file = new FileStream(urlFull1, FileMode.Create))
                        {
                            Hinh2Update.CopyToAsync(file);
                        }
                        sanPham.Hinh2 = Hinh2Update.FileName;
                    }
                    _context.Update(sanPham);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["MaLoai"] = new SelectList(_context.Loai, "MaLoai", "MaLoai", sanPham.MaLoai);
                ViewData["MaNcc"] = new SelectList(_context.NhaCungCap, "MaNcc", "MaNcc", sanPham.MaNcc);
                return View(sanPham);
            }
        }

       
        // POST: SanPhams/Delete/5
        [Route("[controller]/[action]")]
   
        public async Task<IActionResult> Delete(int ?id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var sanPham = await _context.SanPham.FindAsync(id);
                _context.SanPham.Remove(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPham.Any(e => e.MaSp == id);
        }
    }
}
