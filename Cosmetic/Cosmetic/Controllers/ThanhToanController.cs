using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cosmetic.Models;

namespace Cosmetic.Controllers
{
    public class ThanhToanController : Controller
    {
        private readonly MyPhamContext db;
        public ThanhToanController(MyPhamContext context)
        {
            db = context;
        }

        [Route("thanh-toan-1")]
        public IActionResult Index()
        {
            HttpContext.Session.Remove("ThongTin");
            return View(Carts);
        }

        public List<CartItem> Carts
        {
            get
            {
                List<CartItem> myCart = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (myCart == default(List<CartItem>))
                {
                    myCart = new List<CartItem>();
                }

                return myCart;
            }
        }


        [Route("thanh-toan-2")]
        public IActionResult ThanhToan1()
        {
            return View(Carts);

        }

        public List<NguoiNhan> ThongTins
        {
            get
            {
                List<NguoiNhan> myInfo = HttpContext.Session.Get<List<NguoiNhan>>("ThongTin");
                if (myInfo == default(List<NguoiNhan>))
                {
                    myInfo = new List<NguoiNhan>();
                }

                return myInfo;
            }
        }

        //Lưu thông tin người nhận

        [Route("[controller]/[action]")]
        public IActionResult Them(string tennn, string sdtnn, string diachinn, string ghichunn)
        {
            List<NguoiNhan> thongTin = ThongTins;

            NguoiNhan item = thongTin.SingleOrDefault();
            if (item == null)
            {

                item = new NguoiNhan
                {
                    TenNhan = tennn,
                    SDTNhan = sdtnn,
                    DiaChiNhan = diachinn,
                    GhiChu = ghichunn,
                };
                thongTin.Add(item);

            }
            //lưu session
            HttpContext.Session.Set("ThongTin", thongTin);
            return RedirectToAction("ThanhToan1", "ThanhToan");
        }

        //Lưu hóa đơn đặt hàng

        [Route("[controller]/[action]")]
        public IActionResult DatHang(string makh, string hotenkh, string diachikh, string sdt, string tennhan, string sdtnhan, string diachinhan, string ghichunhan)
        {
            HoaDon hd = new HoaDon
            {
                MaKh = makh,
                HoTen = hotenkh,
                DiaChi = diachikh,
                DienThoai = sdt,
                TenNgNhan = tennhan,
                DtngNhan = sdtnhan,
                DiaChiNgNhan = diachinhan,
                GhiChu = ghichunhan,
                NgayDat = DateTime.Now,
                MaTrangThai = 0,
                PhiVanChuyen = 0
            };
            db.HoaDon.Add(hd);

            foreach (var item in Carts)
            {
                SanPham hh = db.SanPham.SingleOrDefault(p => p.MaSp == item.MaHh);
                //Lưu chi tiết hóa đơn
                ChiTietHd cthd = new ChiTietHd
                {
                    MaHd = hd.MaHd,
                    MaSp = item.MaHh,
                    DonGia = hh.DonGia,
                    SoLuong = item.SoLuong,
                };
                db.ChiTietHd.Add(cthd);
                db.SaveChanges();

                //Kiểm tra hàng tồn kho
                KhoHang kho = db.KhoHang.SingleOrDefault(p => p.MaSp == cthd.MaSp);
                if (kho.SoLuong >= cthd.SoLuong)
                {
                    kho.SoLuong = kho.SoLuong - cthd.SoLuong;
                    db.SaveChanges();
                }
                else
                {
                    SanPham sp = db.SanPham.SingleOrDefault(p => p.MaSp == cthd.MaSp);

                    db.ChiTietHd.Remove(cthd);
                    db.HoaDon.Remove(hd);
                    db.SaveChanges();
                    return RedirectToAction("ThanhToan1");
                }
            }

            HttpContext.Session.Remove("GioHang");
            HttpContext.Session.Remove("ThongTin");
            return RedirectToAction("ThanhToan1");

        }

    }
}