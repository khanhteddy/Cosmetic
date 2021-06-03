using System;
using System.Collections.Generic;

namespace Cosmetic.Models
{
    public partial class HoiDap
    {
        public int MaHdap { get; set; }
        public string CauHoi { get; set; }
        public string TraLoi { get; set; }
        public DateTime? NgayDua { get; set; }
        public string MaNv { get; set; }

        public NhanVien MaNvNavigation { get; set; }
    }
}
