using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cosmetic.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            ChuDe = new HashSet<ChuDe>();
            HoaDon = new HashSet<HoaDon>();
            HoiDap = new HashSet<HoiDap>();
            PhanCong = new HashSet<PhanCong>();
        }
        [Display(Name = "Tài khoản")]
        public string MaNv { get; set; }
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }

        public ICollection<ChuDe> ChuDe { get; set; }
        public ICollection<HoaDon> HoaDon { get; set; }
        public ICollection<HoiDap> HoiDap { get; set; }
        public ICollection<PhanCong> PhanCong { get; set; }
    }
}
