using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cosmetic.Models;

namespace Cosmetic.Controllers
{

    public class HoaDonController : Controller
    {
        private readonly MyPhamContext db;
        public HoaDonController(MyPhamContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChiTiet(int? id)
        {
            ViewBag.Ma = id;

            List<ChiTietHd> dscthd = new List<ChiTietHd>();
            if (id.HasValue)
            {
                dscthd = db.ChiTietHd.Where(p => p.MaHd == id)
                    .ToList();

            }
            else
            {
                dscthd = db.ChiTietHd.ToList();
            }
            return View(dscthd.Select(p => new ChiTietHd
            {
                MaCt = p.MaCt,
                MaHd = p.MaHd,
                MaSp = p.MaSp,
                DonGia = p.DonGia,
                SoLuong = p.SoLuong
            }));
        }
    }
}