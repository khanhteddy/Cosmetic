using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Cosmetic.Models
{
    public partial class MyPhamContext : DbContext
    {
        public MyPhamContext()
        {
        }

        public MyPhamContext(DbContextOptions<MyPhamContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BanBe> BanBe { get; set; }
        public virtual DbSet<BinhLuan> BinhLuan { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<ChiTietHd> ChiTietHd { get; set; }
        public virtual DbSet<ChuDe> ChuDe { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<HoiDap> HoiDap { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<KhoHang> KhoHang { get; set; }
        public virtual DbSet<Loai> Loai { get; set; }
        public virtual DbSet<LoaiBlog> LoaiBlog { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCap { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
        public virtual DbSet<PhanCong> PhanCong { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyen { get; set; }
        public virtual DbSet<PhongBan> PhongBan { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }
        public virtual DbSet<ThuongHieu> ThuongHieu { get; set; }
        public virtual DbSet<TrangThai> TrangThai { get; set; }
        public virtual DbSet<TrangWeb> TrangWeb { get; set; }
        public virtual DbSet<YeuThich> YeuThich { get; set; }
        public virtual DbSet<LienHe> LienHe { get; set; }     
            
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                string projectPath = AppDomain.CurrentDomain.BaseDirectory
                .Split(new String[] {@"bin"}, StringSplitOptions.None)[0];
                
                //Chỉ cần thay đổi connectionstring tại appsettings.json
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json")
                .Build();

                string connectionString = configuration.GetConnectionString("WebMyPham");
                /////////////////////////////////////////////////////////////////////////
                //Sử dụng PostgreSQL Entity Framework Core
                //optionsBuilder.UseNpgsql(connectionString);
                
                //Sử dụng Microsoft SQL Server
                optionsBuilder.UseSqlServer(connectionString);
                //////////////////////////////////////////////////////////////////////////
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BanBe>(entity =>
            {
                entity.HasKey(e => e.MaBb);

                entity.Property(e => e.MaBb).HasColumnName("MaBB");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.GhiChu).HasMaxLength(10);

                entity.Property(e => e.HoTen).HasMaxLength(50);

                entity.Property(e => e.MaKh)
                    .HasColumnName("MaKH")
                    .HasMaxLength(50);

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.Property(e => e.NgayGui).HasColumnType("DateTime");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.BanBe)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_BanBe_KhachHang");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.BanBe)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK_BanBe_SanPham");
            });

            modelBuilder.Entity<LienHe>(entity =>
            {
                entity.HasKey(e => e.MaGy);

                entity.Property(e => e.MaGy).HasColumnName("MaGY");

                entity.Property(e => e.DienThoai)
                    .HasColumnName("DIenThoai")
                    .HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.HoTen).HasMaxLength(50);

                entity.Property(e => e.NgayGy)
                    .HasColumnName("NgayGY")
                    .HasColumnType("DateTime");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasKey(e => e.MaBlog);

                entity.Property(e => e.NgayDang).HasColumnType("DateTime");

                entity.Property(e => e.TenBlog).HasMaxLength(50);

                entity.HasOne(d => d.MaLoaiBlogNavigation)
                    .WithMany(p => p.Blog)
                    .HasForeignKey(d => d.MaLoaiBlog)
                    .HasConstraintName("FK_Blog_LoaiBlog");
            });

            modelBuilder.Entity<BinhLuan>(entity =>
            {
                entity.HasKey(e => e.MaBl);

                entity.Property(e => e.MaBl).HasColumnName("MaBL");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.HoTen).HasMaxLength(50);

                entity.Property(e => e.MaKh)
                    .HasColumnName("MaKH")
                    .HasMaxLength(50);

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.Property(e => e.NgayBl)
                    .HasColumnName("NgayBL")
                    .HasColumnType("DateTime");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.BinhLuan)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_BinhLuan_KhachHang");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.BinhLuan)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK_BinhLuan_SanPham");
            });

            modelBuilder.Entity<LoaiBlog>(entity =>
            {
                entity.HasKey(e => e.MaLoaiBlog);
                entity.Property(e => e.MoTa).HasMaxLength(50);
            });

            modelBuilder.Entity<ChiTietHd>(entity =>
            {
                entity.HasKey(e => e.MaCt);

                entity.ToTable("ChiTietHD");

                entity.Property(e => e.MaCt).HasColumnName("MaCT");

                entity.Property(e => e.MaHd).HasColumnName("MaHD");

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.HasOne(d => d.MaHdNavigation)
                    .WithMany(p => p.ChiTietHd)
                    .HasForeignKey(d => d.MaHd)
                    .HasConstraintName("FK_ChiTietHD_HoaDon");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.ChiTietHd)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK_ChiTietHD_SanPham");
            });

            modelBuilder.Entity<ChuDe>(entity =>
            {
                entity.HasKey(e => e.MaCd);

                entity.Property(e => e.MaCd).HasColumnName("MaCD");

                entity.Property(e => e.MaNv)
                    .HasColumnName("MaNV")
                    .HasMaxLength(50);

                entity.Property(e => e.TenCd)
                    .HasColumnName("TenCD")
                    .HasMaxLength(50);

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.ChuDe)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_ChuDe_NhanVien");
            });



            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHd);

                entity.Property(e => e.MaHd).HasColumnName("MaHD");

                entity.Property(e => e.CachThanhToan).HasMaxLength(50);

                entity.Property(e => e.CachVanChuyen).HasMaxLength(50);

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DiaChiNgNhan).HasMaxLength(50);

                entity.Property(e => e.DienThoai).HasMaxLength(50);

                entity.Property(e => e.DtngNhan)
                    .HasColumnName("DTNgNhan")
                    .HasMaxLength(50);

                entity.Property(e => e.GhiChu).HasMaxLength(50);

                entity.Property(e => e.HoTen).HasMaxLength(50);

                entity.Property(e => e.MaKh)
                    .HasColumnName("MaKH")
                    .HasMaxLength(50);

                entity.Property(e => e.MaNv)
                    .HasColumnName("MaNV")
                    .HasMaxLength(50);

                entity.Property(e => e.NgayDat).HasColumnType("DateTime");

                entity.Property(e => e.NgayGiao).HasColumnType("DateTime");

                entity.Property(e => e.TenNgNhan).HasMaxLength(50);

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.HoaDon)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_HoaDon_KhachHang");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.HoaDon)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_HoaDon_NhanVien");

                entity.HasOne(d => d.MaTrangThaiNavigation)
                    .WithMany(p => p.HoaDon)
                    .HasForeignKey(d => d.MaTrangThai)
                    .HasConstraintName("FK_HoaDon_TrangThai");
            });

            modelBuilder.Entity<HoiDap>(entity =>
            {
                entity.HasKey(e => e.MaHdap);

                entity.Property(e => e.MaHdap)
                    .HasColumnName("MaHDap")
                    .ValueGeneratedNever();

                entity.Property(e => e.CauHoi).HasMaxLength(50);

                entity.Property(e => e.MaNv)
                    .HasColumnName("MaNV")
                    .HasMaxLength(50);

                entity.Property(e => e.NgayDua).HasColumnType("DateTime");

                entity.Property(e => e.TraLoi).HasMaxLength(50);

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.HoiDap)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_HoiDap_NhanVien");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh);

                entity.Property(e => e.MaKh)
                    .HasColumnName("MaKH")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.DienThoai).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.HoTen).HasMaxLength(50);

                entity.Property(e => e.MatKhau).HasMaxLength(50);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.RandomKey)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<KhoHang>(entity =>
            {
                entity.HasKey(e => e.MaKho);

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.KhoHang)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK_KhoHang_SanPham");
            });



            modelBuilder.Entity<Loai>(entity =>
            {
                entity.HasKey(e => e.MaLoai);

                entity.Property(e => e.TenLoai).HasMaxLength(50);

                entity.Property(e => e.TenLoaiAlias).HasMaxLength(50);

                entity.Property(e => e.MoTa).HasMaxLength(50);
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.HasKey(e => e.MaNcc);

                entity.Property(e => e.MaNcc)
                    .HasColumnName("MaNCC")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.DienThoai).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Logo).HasMaxLength(50);

                entity.Property(e => e.TenCongTy).HasMaxLength(50);
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNv);

                entity.Property(e => e.MaNv)
                    .HasColumnName("MaNV")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.HoTen).HasMaxLength(50);

                entity.Property(e => e.MatKhau).HasMaxLength(50);
            });

            modelBuilder.Entity<PhanCong>(entity =>
            {
                entity.HasKey(e => e.MaPc);

                entity.Property(e => e.MaPc).HasColumnName("MaPC");

                entity.Property(e => e.MaNv)
                    .HasColumnName("MaNV")
                    .HasMaxLength(50);

                entity.Property(e => e.MaPb)
                    .HasColumnName("MaPB")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.NgayPc)
                    .HasColumnName("NgayPC")
                    .HasColumnType("DateTime");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.PhanCong)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_PhanCong_NhanVien");

                entity.HasOne(d => d.MaPbNavigation)
                    .WithMany(p => p.PhanCong)
                    .HasForeignKey(d => d.MaPb)
                    .HasConstraintName("FK_PhanCong_PhongBan");
            });

            modelBuilder.Entity<PhanQuyen>(entity =>
            {
                entity.HasKey(e => e.MaPq);

                entity.Property(e => e.MaPq).HasColumnName("MaPQ");

                entity.Property(e => e.MaPb)
                    .HasColumnName("MaPB")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaPbNavigation)
                    .WithMany(p => p.PhanQuyen)
                    .HasForeignKey(d => d.MaPb)
                    .HasConstraintName("FK_PhanQuyen_PhongBan");

                entity.HasOne(d => d.MaTrangNavigation)
                    .WithMany(p => p.PhanQuyen)
                    .HasForeignKey(d => d.MaTrang)
                    .HasConstraintName("FK_PhanQuyen_TrangWeb");
            });

            modelBuilder.Entity<PhongBan>(entity =>
            {
                entity.HasKey(e => e.MaPb);

                entity.Property(e => e.MaPb)
                    .HasColumnName("MaPB")
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.TenPb)
                    .HasColumnName("TenPB")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSp);

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.Property(e => e.Hinh).HasMaxLength(50);

                entity.Property(e => e.Hinh2).HasMaxLength(50);

                entity.Property(e => e.MaNcc)
                    .HasColumnName("MaNCC")
                    .HasMaxLength(50);
                
                entity.Property(e => e.TenAlias).HasMaxLength(50);

                entity.Property(e => e.TenSp)
                    .HasColumnName("TenSP")
                    .HasMaxLength(50);


                entity.HasOne(d => d.MaHieuNavigation)
                    .WithMany(p => p.SanPham)
                    .HasForeignKey(d => d.MaHieu)
                    .HasConstraintName("FK_SanPham_ThuongHieu");

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.SanPham)
                    .HasForeignKey(d => d.MaLoai)
                    .HasConstraintName("FK_SanPham_Loai");

                entity.HasOne(d => d.MaNccNavigation)
                    .WithMany(p => p.SanPham)
                    .HasForeignKey(d => d.MaNcc)
                    .HasConstraintName("FK_SanPham_NhaCungCap");
            });

            modelBuilder.Entity<ThuongHieu>(entity =>
            {
                entity.HasKey(e => e.MaHieu);

                entity.Property(e => e.Hinh).HasMaxLength(50);

                entity.Property(e => e.MaHieu).ValueGeneratedOnAdd();

            
                entity.Property(e => e.TenHieu).HasMaxLength(50);

                entity.HasOne(d => d.MaHieuNavigation)
                    .WithOne(p => p.InverseMaHieuNavigation)
                    .HasForeignKey<ThuongHieu>(d => d.MaHieu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ThuongHieu_ThuongHieu");
            });

            modelBuilder.Entity<TrangThai>(entity =>
            {
                entity.HasKey(e => e.MaTrangThai);

                entity.Property(e => e.MaTrangThai).ValueGeneratedNever();

                entity.Property(e => e.MoTa).HasMaxLength(50);

                entity.Property(e => e.TenTrangThai).HasMaxLength(50);
            });

            modelBuilder.Entity<TrangWeb>(entity =>
            {
                entity.HasKey(e => e.MaTrang);

                entity.Property(e => e.TenTrang).HasMaxLength(50);

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<YeuThich>(entity =>
            {
                entity.HasKey(e => e.MaYt);

                entity.Property(e => e.MaYt).HasColumnName("MaYT");

                entity.Property(e => e.MaKh)
                    .HasColumnName("MaKH")
                    .HasMaxLength(50);

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.Property(e => e.MoTa).HasMaxLength(50);

                entity.Property(e => e.NgayChon).HasColumnType("DateTime");

               


                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.YeuThich)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_YeuThich_KhachHang");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.YeuThich)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK_YeuThich_SanPham");
            });
        }
    }
}
