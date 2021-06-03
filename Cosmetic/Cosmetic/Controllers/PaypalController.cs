using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PayPal.v1.Payments;
using Cosmetic.Models;
using EMarket.Services.PayPal;

namespace Cosmetic.Controllers
{
    //public static int mahoadon;
    public class PaypalController : Controller
    {
        private readonly MyPhamContext db = new MyPhamContext();
        private readonly IPayPalPayment _payPal;

        public PaypalController(IPayPalPayment payPal)
        {
            _payPal = payPal;
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
        //[HttpPost]

        [Route("[controller]/[action]")]
        public async Task<IActionResult> PaypalPayment(string makh, string hotenkh, string diachikh, string sdt, string tennhan, string sdtnhan, string diachinhan, string ghichunhan)
        {
            List<CartItem> danhsachhang = HttpContext.Session.Get<List<CartItem>>("GioHang");
            List<Item> items = new List<Item>();
            double total = 0;

            foreach (var x in danhsachhang)
            {
                var gia = Math.Round(x.GiaBan / 23000, 0);
                items.Add(new Item()
                {
                    Name = x.TenHh,
                    Currency = "USD",
                    Price = gia.ToString(),
                    Quantity = x.SoLuong.ToString(),
                    Sku = "sku",
                    Tax = "0"
                });
                total += gia * x.SoLuong;
            }
            HoaDon hd = new HoaDon
            {
                MaKh = makh,
                HoTen = hotenkh,
                DiaChi = "TPHCM",
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
                }
            }
            Payment payment = _payPal.CreatePayment(total, @"http://localhost:61494/thanh-toan-1", @"http://localhost:61494/thanh-toan-1", "sale", items);
            string paypalRedirectUrl = await _payPal.ExecutePayment(payment);
            if (paypalRedirectUrl == "fail")

            {
              
                    hd.MaTrangThai = 0;
                    db.SaveChanges();
                TempData["status"] = "Thanh toán thất bại";
                return RedirectToAction("Fail");
            }
            else
            {
                hd.MaTrangThai = 1;
                db.SaveChanges();
                TempData["status"] = "Thanh toán đơn hàng thanh cong";
                return Redirect(paypalRedirectUrl);
            }
        }
        
        [Route("[controller]/[action]")]
        public IActionResult Success()
        {
          
          
            return Content("Thanh toán thành công");
        }

        [Route("[controller]/[action]")]
        public IActionResult Fail()
        {
            //Tạo đơn hàng trong CSDL trạng thái Chưa thanh toán

            TempData["status"] = "Thanh toán đơn hàng thất bại xin vui lòng thử lại";
            return Content("Thanh toán thất bại");
        }


    }
}