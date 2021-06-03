using System;
using System.Collections.Generic;

namespace Cosmetic.Models
{
    public partial class BanBe
    {
        public int MaBb { get; set; }
        public string MaKh { get; set; }
        public int? MaSp { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public DateTime? NgayGui { get; set; }
        public string GhiChu { get; set; }

        public KhachHang MaKhNavigation { get; set; }
        public SanPham MaSpNavigation { get; set; }
    }
}
