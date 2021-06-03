using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cosmetic.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            BanBe = new HashSet<BanBe>();
            ChiTietHd = new HashSet<ChiTietHd>();
            KhoHang = new HashSet<KhoHang>();
            YeuThich = new HashSet<YeuThich>();
        }
        public string TenSpSeoUrl => TenSp.ToUrlFriendly();
        [Display(Name = "Mã sản phẩm")]
        public int MaSp { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string TenSp { get; set; }
        [Display(Name = "Tên không dấu")]
        public string TenAlias { get; set; }
        [Display(Name = "Mã loại")]
        public int? MaLoai { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        [Display(Name = "Đơn giá")]
        public double? DonGia { get; set; }
        [Display(Name = "Hình")]
        public string Hinh { get; set; }
        [Display(Name = "Hình")]
        public string Hinh2 { get; set; }
        [Display(Name = "Giá cũ")]
        public double? GiaCu { get; set; }
        [Display(Name = "Mã nhà cung cấp")]
        public string MaNcc { get; set; }
        [Display(Name = "Mã thương hiệu")]
        public int? MaHieu { get; set; }

        public bool? SpHot { get; set; }

        public ThuongHieu MaHieuNavigation { get; set; }
        public Loai MaLoaiNavigation { get; set; }
        public NhaCungCap MaNccNavigation { get; set; }
        public ICollection<BanBe> BanBe { get; set; }
        public ICollection<BinhLuan> BinhLuan { get; set; }
        public ICollection<ChiTietHd> ChiTietHd { get; set; }
        public ICollection<KhoHang> KhoHang { get; set; }
        public ICollection<YeuThich> YeuThich { get; set; }
    }
}
