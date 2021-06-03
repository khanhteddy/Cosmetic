using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using Cosmetic.Models;


namespace Cosmetic.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly MyPhamContext db;
        public SanPhamController(MyPhamContext context)
        {
            db = context;
        }


        [Route("san-pham/{loai}")]
        public async Task<IActionResult> Index(string loai, string mucgia, string sapxep, string thuonghieu, int page = 1)
        {

            var qry = db.SanPham.AsNoTracking().OrderBy(p => p.MaSp);
            var model = await PagingList.CreateAsync(qry, 12, page);
            if (loai != null)
            {
                ViewBag.Loai = db.Loai.SingleOrDefault(p => p.TenLoaiSeoUrl == loai);
                Loai qery1 = db.Loai.SingleOrDefault(p => p.TenLoaiSeoUrl == loai);
                var qery = db.SanPham.Where(p => p.MaLoai == qery1.MaLoai).AsNoTracking().OrderBy(p => p.MaSp);
                ThuongHieu qery2 = db.ThuongHieu.SingleOrDefault(p => p.MaHieu.ToString() == thuonghieu);

                if (thuonghieu == "tatca")
                {
                    qery = qery.Where(p => p.MaLoai == qery1.MaLoai).AsNoTracking().OrderBy(p => p.MaSp);
                }
                else
                {
                    bool check = false;
                    var dsThuongHieu = db.ThuongHieu.ToList();
                    foreach (var item in dsThuongHieu)
                    {
                        if (item.MaHieu.ToString() == thuonghieu)
                        {
                            check = true;
                            break;
                        }
                    }

                    if (check == true)
                    {
                        qery = qery.Where(p => p.MaHieu == qery2.MaHieu).AsNoTracking().OrderBy(p => p.MaHieu);
                    }
                }

                switch (mucgia)
                {
                    case "100000":
                        qery = qery.Where(p => p.DonGia < 100000).OrderBy(p => p.MaSp);
                        break;
                    case "500000":
                        qery = qery.Where(p => p.DonGia >= 100000 && p.DonGia < 500000).OrderBy(p => p.MaSp);
                        break;
                    case "1000000":
                        qery = qery.Where(p => p.DonGia >= 500000 && p.DonGia < 1000000).OrderBy(p => p.MaSp);
                        break;
                    case "1000001":
                        qery = qery.Where(p => p.DonGia >= 1000000).OrderBy(p => p.MaSp);
                        break;
                    case "tatca":
                        qery = qery.Where(p => p.MaLoai == qery1.MaLoai).AsNoTracking().OrderBy(p => p.MaSp);
                        break;
                    default:
                        break;
                }

                switch (sapxep)
                {
                    case "tang":
                        qery = qery.OrderBy(p => p.DonGia);
                        break;
                    case "giam":
                        qery = qery.OrderByDescending(p => p.DonGia); ;
                        break;
                    case "tatca":
                        qery = qery.Where(p => p.MaLoai == qery1.MaLoai).AsNoTracking().OrderBy(p => p.MaSp);
                        break;
                    default:
                        break;
                }

                var md = await PagingList.CreateAsync(qery, 99, page);


                return View(md);
            }

            return View(model);
        }

        [Route("thuong-hieu/{hieuurl}")]
        public async Task<IActionResult> Hieu(string hieuurl, int page = 1)
        {

            var qry = db.SanPham.AsNoTracking().OrderBy(p => p.MaSp);
            var model = await PagingList.CreateAsync(qry, 9, page);
            if (hieuurl != null)
            {
                ViewBag.Hieu = db.ThuongHieu.SingleOrDefault(p => p.TenHieuSeoUrl == hieuurl);
                ThuongHieu qery1 = db.ThuongHieu.SingleOrDefault(p => p.TenHieuSeoUrl == hieuurl);
                var qery = db.SanPham.Where(p => p.MaHieu == qery1.MaHieu).AsNoTracking().OrderBy(p => p.MaSp);

                var md = await PagingList.CreateAsync(qery, 9, page);
                return View(md);
            }

            return View();
        }

        [Route("{loai}/{url}")]
        public IActionResult ChiTiet(string loai, string url)
        {
            if (loai != null)
            {
                ViewBag.Loai = db.Loai.SingleOrDefault(p => p.TenLoaiSeoUrl == loai);
            }
            SanPham hh = db.SanPham.SingleOrDefault(p => p.TenSpSeoUrl == url);
            if (hh == null)
            {
                return RedirectToAction("Index");
            }
            return View(hh);
        }

        public IActionResult timKiem()
        {
            return View();
        }

        [Route("[controller]/[action]")]
        public IActionResult timSp()
        {
            string key = Request.Form["keysearch"].ToString();
            var sanPham = (from sp in db.SanPham
                           where sp.TenSp.Contains(key) && key != ""
                           select new SanPham
                           {
                               MaSp = sp.MaSp,
                               TenSp = sp.TenSp,
                               TenAlias = sp.TenAlias,
                               MaLoai = sp.MaLoai,
                               MoTa = sp.MoTa,
                               DonGia = sp.DonGia,
                               Hinh = sp.Hinh,
                               GiaCu = sp.GiaCu,
                               MaNcc = sp.MaNcc,
                               MaHieu = sp.MaHieu
                           }).ToList();
            return View(sanPham);
        }

        [Route("[controller]/[action]")]
        public IActionResult BinhLuan(string makh, int masp, string hoten, string email, string noidung, string loais, string urls)
        {
            BinhLuan bl = new BinhLuan
            {
                MaSp = masp,
                MaKh = makh,
                NgayBl = DateTime.Now,
                HoTen = hoten,
                Email = email,
                NoiDung = noidung,

            };

            db.BinhLuan.Add(bl);
            db.SaveChanges();
            return RedirectToAction("ChiTiet", "SanPham", new { url = urls, loai = loais });
        }
    }
}