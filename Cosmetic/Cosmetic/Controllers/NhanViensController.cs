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
    public class NhanViensController : Controller
    {
        private readonly MyPhamContext _context;

        public NhanViensController(MyPhamContext context)
        {
            _context = context;
        }

        // GET: NhanViens
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                return View(await _context.NhanVien.ToListAsync());
            }
        }

        // GET: NhanViens/Details/5
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

                var nhanVien = await _context.NhanVien
                    .FirstOrDefaultAsync(m => m.MaNv == id);
                if (nhanVien == null)
                {
                    return NotFound();
                }

                return View(nhanVien);
            }
        }

        // GET: NhanViens/Create
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

        // POST: NhanViens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("[controller]/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNv,HoTen,Email,MatKhau")] NhanVien nhanVien)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(nhanVien);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(nhanVien);
            }
        }

        // GET: NhanViens/Edit/5
        [Route("[controller]/[action]")]
        public IActionResult Edit(string id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var nhanVien = _context.NhanVien.SingleOrDefault(i => i.MaNv == id);
                if (id == null)
                {
                    return NotFound();
                }

                
                if (nhanVien == null)
                {
                    return NotFound();
                }
                return View(nhanVien);
            }
        }

        // POST: NhanViens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("[controller]/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaNv,HoTen,Email,MatKhau")] NhanVien nhanVien)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                

                if (ModelState.IsValid)
                {
                   
                        _context.Update(nhanVien);
                        await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(nhanVien);
            }
        }

  

        // POST: NhanViens/Delete/5
        [Route("[controller]/[action]")]

        public async Task<IActionResult> Delete(string id)
        {
            if (HttpContext.Session.Get<NhanVien>("MaNv") == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var nhanVien = await _context.NhanVien.FindAsync(id);
                _context.NhanVien.Remove(nhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool NhanVienExists(string id)
        {
            return _context.NhanVien.Any(e => e.MaNv == id);
        }
    }
}
