using System;
using System.Collections.Generic;

namespace Cosmetic.Models
{
    public partial class KhuyenMai
    {
        public int MaKm { get; set; }
        public string CodeKm { get; set; }
        public string LoaiKm { get; set; }
        public string MaKh { get; set; }

        public KhachHang MaKhNavigation { get; set; }
    }
}
