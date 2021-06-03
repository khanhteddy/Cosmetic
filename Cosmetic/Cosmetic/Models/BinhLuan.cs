using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cosmetic.Models
{
    public partial class BinhLuan
    {
        [Display(Name = "Mã bình luận")]
        public int MaBl { get; set; }
        [Display(Name = "Mã sản phẩm")]
        public int? MaSp { get; set; }
        [Display(Name = "Tài khoản")]
        public string MaKh { get; set; }
        [Display(Name = "Ngày")]
        public DateTime NgayBl { get; set; }
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }

        public KhachHang MaKhNavigation { get; set; }
        public SanPham MaSpNavigation { get; set; }
    }
}
