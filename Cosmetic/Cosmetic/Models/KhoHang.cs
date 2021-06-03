using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cosmetic.Models
{
    public partial class KhoHang
    {
        [Display(Name = "Mã kho")]
        public int MaKho { get; set; }
        [Display(Name = "Mã sản phẩm")]
        public int? MaSp { get; set; }
        [Display(Name = "Số lượng")]
        public int? SoLuong { get; set; }

        public SanPham MaSpNavigation { get; set; }
    }
}
