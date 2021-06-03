using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cosmetic.Models
{
    public partial class ThuongHieu
    {
        public ThuongHieu()
        {
            SanPham = new HashSet<SanPham>();
        }
        [Display(Name = "Mã thương hiệu")]
        public int MaHieu { get; set; }
        [Display(Name = "Tên thương hiệu")]
        public string TenHieu { get; set; }
        public string TenHieuSeoUrl => TenHieu.ToUrlFriendly();
        [Display(Name = "Hình")]
        public string Hinh { get; set; }
        public ThuongHieu MaHieuNavigation { get; set; }
        public ThuongHieu InverseMaHieuNavigation { get; set; }
        public ICollection<SanPham> SanPham { get; set; }
    }
}
