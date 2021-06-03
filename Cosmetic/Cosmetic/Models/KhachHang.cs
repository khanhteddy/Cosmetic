using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cosmetic.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            BanBe = new HashSet<BanBe>();
            HoaDon = new HashSet<HoaDon>();
            YeuThich = new HashSet<YeuThich>();
        }
        [Display(Name = "Tài khoản")]
        public string MaKh { get; set; }
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }
        [Display(Name = "Giới tính")]
        public bool? GioiTinh { get; set; }
        [Display(Name = "Ngày sinh")]
        public DateTime? NgaySinh { get; set; }
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
        [Display(Name = "Điện thoại")]
        public string DienThoai { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Hiệu lực")]
        public bool? HieuLuc { get; set; }
        [Display(Name = "Vai trò")]
        public int? VaiTro { get; set; }
        public string RandomKey { get; set; }

        public ICollection<BanBe> BanBe { get; set; }
        public ICollection<BinhLuan> BinhLuan { get; set; }
        public ICollection<HoaDon> HoaDon { get; set; }
        public ICollection<YeuThich> YeuThich { get; set; }

        public string AuthyId { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
       
    }
}
