using System;
using System.Collections.Generic;

namespace Cosmetic.Models
{
    public partial class ChuDe
    {
        public ChuDe()
        {
           
        }

        public int MaCd { get; set; }
        public string TenCd { get; set; }
        public string MaNv { get; set; }

        public NhanVien MaNvNavigation { get; set; }
        
    }
}
