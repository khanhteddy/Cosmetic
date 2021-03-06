USE [MyPham]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BanBe]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BanBe](
	[MaBB] [int] IDENTITY(1,1) NOT NULL,
	[MaKH] [nvarchar](50) NULL,
	[MaSP] [int] NULL,
	[HoTen] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[NgayGui] [datetime] NULL,
	[GhiChu] [nchar](10) NULL,
 CONSTRAINT [PK_BanBe] PRIMARY KEY CLUSTERED 
(
	[MaBB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BinhLuan]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BinhLuan](
	[MaBL] [int] IDENTITY(1,1) NOT NULL,
	[MaSP] [int] NULL,
	[MaKH] [nvarchar](50) NULL,
	[NgayBL] [datetime] NULL,
	[HoTen] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[NoiDung] [nvarchar](max) NULL,
 CONSTRAINT [PK_BinhLuan] PRIMARY KEY CLUSTERED 
(
	[MaBL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[MaBlog] [int] IDENTITY(1,1) NOT NULL,
	[TenBlog] [nvarchar](50) NULL,
	[NoiDung] [nvarchar](max) NULL,
	[NgayDang] [datetime] NULL,
	[MaLoaiBlog] [int] NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[MaBlog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietHD]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHD](
	[MaCT] [int] IDENTITY(0,1) NOT NULL,
	[MaHD] [int] NULL,
	[MaSP] [int] NULL,
	[DonGia] [float] NULL,
	[SoLuong] [int] NULL,
	[GiamGia] [float] NULL,
 CONSTRAINT [PK_ChiTietHD] PRIMARY KEY CLUSTERED 
(
	[MaCT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChuDe]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChuDe](
	[MaCD] [int] IDENTITY(1,1) NOT NULL,
	[TenCD] [nvarchar](50) NULL,
	[MaNV] [nvarchar](50) NULL,
 CONSTRAINT [PK_ChuDe] PRIMARY KEY CLUSTERED 
(
	[MaCD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[MaHD] [int] IDENTITY(1,1) NOT NULL,
	[MaKH] [nvarchar](50) NULL,
	[NgayDat] [datetime] NULL,
	[NgayGiao] [datetime] NULL,
	[MaNV] [nvarchar](50) NULL,
	[HoTen] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](50) NOT NULL,
	[CachThanhToan] [nvarchar](50) NULL,
	[CachVanChuyen] [nvarchar](50) NULL,
	[PhiVanChuyen] [float] NULL,
	[MaTrangThai] [int] NULL,
	[GhiChu] [nvarchar](50) NULL,
	[DienThoai] [nvarchar](50) NULL,
	[TenNgNhan] [nvarchar](50) NULL,
	[DTNgNhan] [nvarchar](50) NULL,
	[DiaChiNgNhan] [nvarchar](50) NULL,
	[TongTien] [float] NULL,
 CONSTRAINT [PK_HoaDon] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoiDap]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoiDap](
	[MaHDap] [int] NOT NULL,
	[CauHoi] [nvarchar](50) NULL,
	[TraLoi] [nvarchar](50) NULL,
	[NgayDua] [datetime] NULL,
	[MaNV] [nvarchar](50) NULL,
 CONSTRAINT [PK_HoiDap] PRIMARY KEY CLUSTERED 
(
	[MaHDap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[MaKH] [nvarchar](50) NOT NULL,
	[MatKhau] [nvarchar](100) NULL,
	[HoTen] [nvarchar](50) NULL,
	[GioiTinh] [bit] NULL,
	[NgaySinh] [date] NULL,
	[DiaChi] [nvarchar](50) NULL,
	[DienThoai] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[HieuLuc] [bit] NULL,
	[VaiTro] [int] NULL,
	[RandomKey] [varchar](50) NULL,
	[AuthyId] [varchar](50) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[PhoneNumberConfirmed] [bit] NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhoHang]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhoHang](
	[MaKho] [int] IDENTITY(1,1) NOT NULL,
	[MaSP] [int] NULL,
	[SoLuong] [int] NULL,
 CONSTRAINT [PK_KhoHang] PRIMARY KEY CLUSTERED 
(
	[MaKho] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhuyenMai]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhuyenMai](
	[MaKM] [int] IDENTITY(1,1) NOT NULL,
	[codeKM] [nvarchar](50) NULL,
	[LoaiKM] [nvarchar](50) NULL,
	[MaKH] [nvarchar](50) NULL,
 CONSTRAINT [PK_KhuyenMai] PRIMARY KEY CLUSTERED 
(
	[MaKM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LienHe]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LienHe](
	[MaGY] [int] IDENTITY(1,1) NOT NULL,
	[TieuDe] [nvarchar](max) NULL,
	[NoiDung] [nvarchar](max) NULL,
	[NgayGY] [datetime] NULL,
	[HoTen] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[DienThoai] [nvarchar](50) NULL,
 CONSTRAINT [PK_LienHe] PRIMARY KEY CLUSTERED 
(
	[MaGY] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Loai]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loai](
	[MaLoai] [int] IDENTITY(1,1) NOT NULL,
	[TenLoai] [nvarchar](50) NULL,
	[TenLoaiAlias] [nvarchar](50) NULL,
	[MoTa] [nvarchar](max) NULL,
 CONSTRAINT [PK_Loai] PRIMARY KEY CLUSTERED 
(
	[MaLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiBlog]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiBlog](
	[MaLoaiBlog] [int] IDENTITY(1,1) NOT NULL,
	[TenLoaiBlog] [nvarchar](max) NULL,
	[MoTa] [nvarchar](50) NULL,
 CONSTRAINT [PK_LoaiBlog] PRIMARY KEY CLUSTERED 
(
	[MaLoaiBlog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhaCungCap]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhaCungCap](
	[MaNCC] [nvarchar](50) NOT NULL,
	[TenCongTy] [nvarchar](50) NULL,
	[Logo] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[DienThoai] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[MoTa] [nvarchar](max) NULL,
 CONSTRAINT [PK_NhaCungCap] PRIMARY KEY CLUSTERED 
(
	[MaNCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNV] [nvarchar](50) NOT NULL,
	[HoTen] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[MatKhau] [nvarchar](50) NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhanCong]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhanCong](
	[MaPC] [int] IDENTITY(1,1) NOT NULL,
	[MaNV] [nvarchar](50) NULL,
	[MaPB] [varchar](7) NULL,
	[NgayPC] [datetime] NULL,
	[HieuLuc] [bit] NULL,
 CONSTRAINT [PK_PhanCong] PRIMARY KEY CLUSTERED 
(
	[MaPC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhanQuyen]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhanQuyen](
	[MaPQ] [int] IDENTITY(1,1) NOT NULL,
	[MaPB] [varchar](7) NULL,
	[MaTrang] [int] NULL,
	[Them] [bit] NULL,
	[Sua] [bit] NULL,
	[Xoa] [bit] NULL,
	[Xem] [bit] NULL,
 CONSTRAINT [PK_PhanQuyen] PRIMARY KEY CLUSTERED 
(
	[MaPQ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhongBan]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhongBan](
	[MaPB] [varchar](7) NOT NULL,
	[TenPB] [nvarchar](50) NULL,
	[ThongTin] [nvarchar](max) NULL,
 CONSTRAINT [PK_PhongBan] PRIMARY KEY CLUSTERED 
(
	[MaPB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSP] [int] IDENTITY(1,1) NOT NULL,
	[MaHieu] [int] NULL,
	[TenSP] [nvarchar](50) NULL,
	[TenAlias] [nvarchar](50) NULL,
	[MaLoai] [int] NULL,
	[MoTa] [nvarchar](max) NULL,
	[DonGia] [float] NULL,
	[Hinh] [nvarchar](50) NULL,
	[Hinh2] [nvarchar](50) NULL,
	[GiaCu] [float] NULL,
	[MaNCC] [nvarchar](50) NULL,
	[SpHot] [bit] NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThuongHieu]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThuongHieu](
	[MaHieu] [int] IDENTITY(1,1) NOT NULL,
	[TenHieu] [nvarchar](50) NULL,
	[Hinh] [nvarchar](50) NULL,
 CONSTRAINT [PK_ThuongHieu] PRIMARY KEY CLUSTERED 
(
	[MaHieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrangThai]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrangThai](
	[MaTrangThai] [int] NOT NULL,
	[TenTrangThai] [nvarchar](50) NULL,
	[MoTa] [nvarchar](50) NULL,
 CONSTRAINT [PK_TrangThai] PRIMARY KEY CLUSTERED 
(
	[MaTrangThai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrangWeb]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrangWeb](
	[MaTrang] [int] IDENTITY(1,1) NOT NULL,
	[TenTrang] [nvarchar](50) NULL,
	[URL] [nvarchar](250) NULL,
 CONSTRAINT [PK_TrangWeb] PRIMARY KEY CLUSTERED 
(
	[MaTrang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[YeuThich]    Script Date: 1/7/2021 10:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YeuThich](
	[MaYT] [int] IDENTITY(1,1) NOT NULL,
	[MaSP] [int] NULL,
	[MaKH] [nvarchar](50) NULL,
	[NgayChon] [datetime] NULL,
	[MoTa] [nvarchar](50) NULL,
 CONSTRAINT [PK_YeuThich] PRIMARY KEY CLUSTERED 
(
	[MaYT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[KhachHang] ADD  DEFAULT ((0)) FOR [PhoneNumberConfirmed]
GO
ALTER TABLE [dbo].[BanBe]  WITH CHECK ADD  CONSTRAINT [FK_BanBe_KhachHang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[BanBe] CHECK CONSTRAINT [FK_BanBe_KhachHang]
GO
ALTER TABLE [dbo].[BanBe]  WITH CHECK ADD  CONSTRAINT [FK_BanBe_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[BanBe] CHECK CONSTRAINT [FK_BanBe_SanPham]
GO
ALTER TABLE [dbo].[BinhLuan]  WITH CHECK ADD  CONSTRAINT [FK_BinhLuan_KhachHang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[BinhLuan] CHECK CONSTRAINT [FK_BinhLuan_KhachHang]
GO
ALTER TABLE [dbo].[BinhLuan]  WITH CHECK ADD  CONSTRAINT [FK_BinhLuan_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[BinhLuan] CHECK CONSTRAINT [FK_BinhLuan_SanPham]
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_LoaiBlog] FOREIGN KEY([MaLoaiBlog])
REFERENCES [dbo].[LoaiBlog] ([MaLoaiBlog])
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_LoaiBlog]
GO
ALTER TABLE [dbo].[ChiTietHD]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHD_HoaDon] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HoaDon] ([MaHD])
GO
ALTER TABLE [dbo].[ChiTietHD] CHECK CONSTRAINT [FK_ChiTietHD_HoaDon]
GO
ALTER TABLE [dbo].[ChiTietHD]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHD_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[ChiTietHD] CHECK CONSTRAINT [FK_ChiTietHD_SanPham]
GO
ALTER TABLE [dbo].[ChuDe]  WITH CHECK ADD  CONSTRAINT [FK_ChuDe_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[ChuDe] CHECK CONSTRAINT [FK_ChuDe_NhanVien]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_KhachHang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_KhachHang]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_NhanVien]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_TrangThai] FOREIGN KEY([MaTrangThai])
REFERENCES [dbo].[TrangThai] ([MaTrangThai])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_TrangThai]
GO
ALTER TABLE [dbo].[HoiDap]  WITH CHECK ADD  CONSTRAINT [FK_HoiDap_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[HoiDap] CHECK CONSTRAINT [FK_HoiDap_NhanVien]
GO
ALTER TABLE [dbo].[KhoHang]  WITH CHECK ADD  CONSTRAINT [FK_KhoHang_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[KhoHang] CHECK CONSTRAINT [FK_KhoHang_SanPham]
GO
ALTER TABLE [dbo].[KhuyenMai]  WITH CHECK ADD  CONSTRAINT [FK_KhuyenMai_KhachHang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[KhuyenMai] CHECK CONSTRAINT [FK_KhuyenMai_KhachHang]
GO
ALTER TABLE [dbo].[PhanCong]  WITH CHECK ADD  CONSTRAINT [FK_PhanCong_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[PhanCong] CHECK CONSTRAINT [FK_PhanCong_NhanVien]
GO
ALTER TABLE [dbo].[PhanCong]  WITH CHECK ADD  CONSTRAINT [FK_PhanCong_PhongBan] FOREIGN KEY([MaPB])
REFERENCES [dbo].[PhongBan] ([MaPB])
GO
ALTER TABLE [dbo].[PhanCong] CHECK CONSTRAINT [FK_PhanCong_PhongBan]
GO
ALTER TABLE [dbo].[PhanQuyen]  WITH CHECK ADD  CONSTRAINT [FK_PhanQuyen_PhongBan] FOREIGN KEY([MaPB])
REFERENCES [dbo].[PhongBan] ([MaPB])
GO
ALTER TABLE [dbo].[PhanQuyen] CHECK CONSTRAINT [FK_PhanQuyen_PhongBan]
GO
ALTER TABLE [dbo].[PhanQuyen]  WITH CHECK ADD  CONSTRAINT [FK_PhanQuyen_TrangWeb] FOREIGN KEY([MaTrang])
REFERENCES [dbo].[TrangWeb] ([MaTrang])
GO
ALTER TABLE [dbo].[PhanQuyen] CHECK CONSTRAINT [FK_PhanQuyen_TrangWeb]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_Loai] FOREIGN KEY([MaLoai])
REFERENCES [dbo].[Loai] ([MaLoai])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_Loai]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_NhaCungCap] FOREIGN KEY([MaNCC])
REFERENCES [dbo].[NhaCungCap] ([MaNCC])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_NhaCungCap]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_ThuongHieu] FOREIGN KEY([MaHieu])
REFERENCES [dbo].[ThuongHieu] ([MaHieu])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_ThuongHieu]
GO
ALTER TABLE [dbo].[ThuongHieu]  WITH CHECK ADD  CONSTRAINT [FK_ThuongHieu_ThuongHieu] FOREIGN KEY([MaHieu])
REFERENCES [dbo].[ThuongHieu] ([MaHieu])
GO
ALTER TABLE [dbo].[ThuongHieu] CHECK CONSTRAINT [FK_ThuongHieu_ThuongHieu]
GO
ALTER TABLE [dbo].[YeuThich]  WITH CHECK ADD  CONSTRAINT [FK_YeuThich_KhachHang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[YeuThich] CHECK CONSTRAINT [FK_YeuThich_KhachHang]
GO
ALTER TABLE [dbo].[YeuThich]  WITH CHECK ADD  CONSTRAINT [FK_YeuThich_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[YeuThich] CHECK CONSTRAINT [FK_YeuThich_SanPham]
GO
