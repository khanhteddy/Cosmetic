using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cosmetic.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            ChiTietHd = new HashSet<ChiTietHd>();
        }
        [Display(Name = "Mã hóa đơn")]
        public int MaHd { get; set; }
        [Display(Name = "Mã khách hàng")]
        public string MaKh { get; set; }
        [Display(Name = "Ngày đặt")]
        public DateTime? NgayDat { get; set; }
        [Display(Name = "Ngày giao")]
        public DateTime? NgayGiao { get; set; }
        [Display(Name = "Mã nhân viên")]
        public string MaNv { get; set; }
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
        [Display(Name = "Cách thanh toán")]
        public string CachThanhToan { get; set; }
        [Display(Name = "Cách vận chuyển")]
        public string CachVanChuyen { get; set; }
        [Display(Name = "Phí vận chuyển")]
        public double? PhiVanChuyen { get; set; }
        [Display(Name = "Trạng thái")]
        public int? MaTrangThai { get; set; }
        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }
        [Display(Name = "Điện thoại")]
        public string DienThoai { get; set; }
        [Display(Name = "Tên khách hàng")]
        public string TenNgNhan { get; set; }
        [Display(Name = "Điện thoại")]
        public string DtngNhan { get; set; }
        [Display(Name = "Địa chỉ")]
        public string DiaChiNgNhan { get; set; }

        public KhachHang MaKhNavigation { get; set; }
        public NhanVien MaNvNavigation { get; set; }
        public TrangThai MaTrangThaiNavigation { get; set; }
        public ICollection<ChiTietHd> ChiTietHd { get; set; }
    }
}
