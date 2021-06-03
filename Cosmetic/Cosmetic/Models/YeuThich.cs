using System;
using System.Collections.Generic;

namespace Cosmetic.Models
{
    public partial class YeuThich
    {
        public int MaYt { get; set; }
        public int? MaSp { get; set; }
        public string MaKh { get; set; }
        public DateTime? NgayChon { get; set; }
        public string MoTa { get; set; }
        public KhachHang MaKhNavigation { get; set; }
        public SanPham MaSpNavigation { get; set; }
    }
}
