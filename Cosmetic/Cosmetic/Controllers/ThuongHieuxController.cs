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
    public class ThuongHieuxController : Controller
    {
        private readonly MyPhamContext _context;

        public ThuongHieuxController(MyPhamContext context)
        {
            _context = context;
        }

        // GET: ThuongHieux
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var myPhamContext = _context.ThuongHieu.Include(t => t.MaHieuNavigation);
                return View(await myPhamContext.ToListAsync());
            }
        }

        // GET: ThuongHieux/Details/5
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

                var thuongHieu = await _context.ThuongHieu
                    .Include(t => t.MaHieuNavigation)
                    .FirstOrDefaultAsync(m => m.MaHieu == id);
                if (thuongHieu == null)
                {
                    return NotFound();
                }

                return View(thuongHieu);
            }
        }

        // GET: ThuongHieux/Create
        [Route("[controller]/[action]")]
        public IActionResult Create()
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                ViewData["MaHieu"] = new SelectList(_context.ThuongHieu, "MaHieu", "MaHieu");
                return View();
            }
        }

        // POST: ThuongHieux/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("[controller]/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ThuongHieu thuongHieu, IFormFile Hinh)
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
                        string urlFull = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", "beauty", Hinh.FileName);
                        using (var file = new FileStream(urlFull, FileMode.Create))
                        {
                            await Hinh.CopyToAsync(file);
                        }
                        thuongHieu.Hinh = Hinh.FileName;
                    }
                    _context.Add(thuongHieu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["MaHieu"] = new SelectList(_context.ThuongHieu, "MaHieu", "MaHieu", thuongHieu.MaHieu);
                return View(thuongHieu);
            }
        }

        // GET: ThuongHieux/Edit/5
        [Route("[controller]/[action]")]
        public IActionResult Edit(int? id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var thuongHieu = _context.ThuongHieu.SingleOrDefault(th => th.MaHieu == id);
                if (id == null)
                {
                    return RedirectToAction("Index");
                }


                if (thuongHieu == null)
                {
                    return RedirectToAction("Index");
                }
                ViewData["MaHieu"] = new SelectList(_context.ThuongHieu, "MaHieu", "MaHieu", thuongHieu.MaHieu);
                return View(thuongHieu);
            }
        }

        // POST: ThuongHieux/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("[controller]/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ThuongHieu thuongHieu, IFormFile Hinh3Update)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
      
                if (ModelState.IsValid)
                {
                    if (Hinh3Update != null)
                    {
                        string url3Full = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", "beauty", Hinh3Update.FileName);
                        using (var file = new FileStream(url3Full, FileMode.Create))
                        {
                            Hinh3Update.CopyToAsync(file);
                        }
                       thuongHieu.Hinh = Hinh3Update.FileName;

                    }
                    _context.Update(thuongHieu);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                ViewData["MaHieu"] = new SelectList(_context.ThuongHieu, "MaHieu", "MaHieu", thuongHieu.MaHieu);
                return View(thuongHieu);
            }
        }

       
        [Route("[controller]/[action]")]
       
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var thuongHieu = await _context.ThuongHieu.FindAsync(id);
                _context.ThuongHieu.Remove(thuongHieu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool ThuongHieuExists(int id)
        {
            return _context.ThuongHieu.Any(e => e.MaHieu == id);
        }
    }
}
