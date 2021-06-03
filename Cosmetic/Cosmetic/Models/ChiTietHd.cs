using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cosmetic.Models
{
    public partial class ChiTietHd
    {
        [Display(Name = "Mã chi tiết")]
        public int MaCt { get; set; }
        [Display(Name = "Mã hóa đơn")]
        public int? MaHd { get; set; }
        [Display(Name = "Mã sản phẩm")]
        public int? MaSp { get; set; }
        [Display(Name = "Đơn giá")]
        public double? DonGia { get; set; }
        [Display(Name = "Số lượng")]
        public int? SoLuong { get; set; }
        [Display(Name = "Giảm giá")]
        public double? GiamGia { get; set; }

        public HoaDon MaHdNavigation { get; set; }
        public SanPham MaSpNavigation { get; set; }
    }
}
