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
    public class LoaisController : Controller
    {
        private readonly MyPhamContext _context;

        public LoaisController(MyPhamContext context)
        {
            _context = context;
        }

        // GET: Loais
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                return View(await _context.Loai.ToListAsync());
            }
        }

        // GET: Loais/Details/5
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

                var loai = await _context.Loai
                    .FirstOrDefaultAsync(m => m.MaLoai == id);
                if (loai == null)
                {
                    return NotFound();
                }

                return View(loai);
            }
        }

        // GET: Loais/Create
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

        // POST: Loais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("[controller]/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoai,TenLoai,TenLoaiAlias,MoTa")] Loai loai)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(loai);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(loai);
            }
        }

        // GET: Loais/Edit/5
        [Route("[controller]/[action]")]
        public IActionResult Edit(int? id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var loai = _context.Loai.SingleOrDefault(l => l.MaLoai == id);
                if (id == null)
                {
                    return RedirectToAction("Index");
                }


                if (loai == null)
                {
                    return RedirectToAction("Index");
                }
                return View(loai);
            }
        }

        // POST: Loais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("[controller]/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Loai loai)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
               
                if (ModelState.IsValid)
                {
                    
                        _context.Update(loai);
                        await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(loai);
            }
        }

        // POST: Loais/Delete/5
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var loai = await _context.Loai.FindAsync(id);
                _context.Loai.Remove(loai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool LoaiExists(int id)
        {
            return _context.Loai.Any(e => e.MaLoai == id);
        }
    }
}
