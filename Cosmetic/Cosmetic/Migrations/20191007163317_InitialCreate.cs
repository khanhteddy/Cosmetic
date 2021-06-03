using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Cosmetic.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKH = table.Column<string>(maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(maxLength: 50, nullable: true),
                    HoTen = table.Column<string>(maxLength: 50, nullable: true),
                    GioiTinh = table.Column<bool>(nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: true),
                    DiaChi = table.Column<string>(maxLength: 50, nullable: true),
                    DienThoai = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    HieuLuc = table.Column<bool>(nullable: true),
                    VaiTro = table.Column<int>(nullable: true),
                    RandomKey = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.MaKH);
                });

            migrationBuilder.CreateTable(
                name: "LienHe",
                columns: table => new
                {
                    MaGY = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TieuDe = table.Column<string>(nullable: true),
                    NoiDung = table.Column<string>(nullable: true),
                    NgayGY = table.Column<DateTime>(type: "timestamp", nullable: true),
                    HoTen = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    DIenThoai = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LienHe", x => x.MaGY);
                });

            migrationBuilder.CreateTable(
                name: "Loai",
                columns: table => new
                {
                    MaLoai = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TenLoai = table.Column<string>(maxLength: 50, nullable: true),
                    TenLoaiAlias = table.Column<string>(maxLength: 50, nullable: true),
                    MoTa = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loai", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "LoaiBlog",
                columns: table => new
                {
                    MaLoaiBlog = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TenLoaiBlog = table.Column<string>(nullable: true),
                    MoTa = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiBlog", x => x.MaLoaiBlog);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    MaNCC = table.Column<string>(maxLength: 50, nullable: false),
                    TenCongTy = table.Column<string>(maxLength: 50, nullable: true),
                    Logo = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    DienThoai = table.Column<string>(maxLength: 50, nullable: true),
                    DiaChi = table.Column<string>(maxLength: 50, nullable: true),
                    MoTa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCap", x => x.MaNCC);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    MaNV = table.Column<string>(maxLength: 50, nullable: false),
                    HoTen = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    MatKhau = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.MaNV);
                });

            migrationBuilder.CreateTable(
                name: "PhongBan",
                columns: table => new
                {
                    MaPB = table.Column<string>(unicode: false, maxLength: 7, nullable: false),
                    TenPB = table.Column<string>(maxLength: 50, nullable: true),
                    ThongTin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBan", x => x.MaPB);
                });

            migrationBuilder.CreateTable(
                name: "ThuongHieu",
                columns: table => new
                {
                    MaHieu = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TenHieu = table.Column<string>(maxLength: 50, nullable: true),
                    Hinh = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuongHieu", x => x.MaHieu);
                });

            migrationBuilder.CreateTable(
                name: "TrangThai",
                columns: table => new
                {
                    MaTrangThai = table.Column<int>(nullable: false),
                    TenTrangThai = table.Column<string>(maxLength: 50, nullable: true),
                    MoTa = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThai", x => x.MaTrangThai);
                });

            migrationBuilder.CreateTable(
                name: "TrangWeb",
                columns: table => new
                {
                    MaTrang = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TenTrang = table.Column<string>(maxLength: 50, nullable: true),
                    URL = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangWeb", x => x.MaTrang);
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    MaBlog = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TenBlog = table.Column<string>(maxLength: 50, nullable: true),
                    NoiDung = table.Column<string>(nullable: true),
                    NgayDang = table.Column<DateTime>(type: "timestamp", nullable: true),
                    MaLoaiBlog = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.MaBlog);
                    table.ForeignKey(
                        name: "FK_Blog_LoaiBlog",
                        column: x => x.MaLoaiBlog,
                        principalTable: "LoaiBlog",
                        principalColumn: "MaLoaiBlog",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChuDe",
                columns: table => new
                {
                    MaCD = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TenCD = table.Column<string>(maxLength: 50, nullable: true),
                    MaNV = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuDe", x => x.MaCD);
                    table.ForeignKey(
                        name: "FK_ChuDe_NhanVien",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoiDap",
                columns: table => new
                {
                    MaHDap = table.Column<int>(nullable: false),
                    CauHoi = table.Column<string>(maxLength: 50, nullable: true),
                    TraLoi = table.Column<string>(maxLength: 50, nullable: true),
                    NgayDua = table.Column<DateTime>(type: "timestamp", nullable: true),
                    MaNV = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoiDap", x => x.MaHDap);
                    table.ForeignKey(
                        name: "FK_HoiDap_NhanVien",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhanCong",
                columns: table => new
                {
                    MaPC = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    MaNV = table.Column<string>(maxLength: 50, nullable: true),
                    MaPB = table.Column<string>(unicode: false, maxLength: 7, nullable: true),
                    NgayPC = table.Column<DateTime>(type: "timestamp", nullable: true),
                    HieuLuc = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanCong", x => x.MaPC);
                    table.ForeignKey(
                        name: "FK_PhanCong_NhanVien",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhanCong_PhongBan",
                        column: x => x.MaPB,
                        principalTable: "PhongBan",
                        principalColumn: "MaPB",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    MaSP = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TenSP = table.Column<string>(maxLength: 50, nullable: true),
                    TenAlias = table.Column<string>(maxLength: 50, nullable: true),
                    MaLoai = table.Column<int>(nullable: true),
                    MoTa = table.Column<string>(nullable: true),
                    DonGia = table.Column<double>(nullable: true),
                    Hinh = table.Column<string>(maxLength: 50, nullable: true),
                    Hinh2 = table.Column<string>(maxLength: 50, nullable: true),
                    GiaCu = table.Column<double>(nullable: true),
                    MaNCC = table.Column<string>(maxLength: 50, nullable: true),
                    MaHieu = table.Column<int>(nullable: true),
                    SpHot = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.MaSP);
                    table.ForeignKey(
                        name: "FK_SanPham_ThuongHieu",
                        column: x => x.MaHieu,
                        principalTable: "ThuongHieu",
                        principalColumn: "MaHieu",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SanPham_Loai",
                        column: x => x.MaLoai,
                        principalTable: "Loai",
                        principalColumn: "MaLoai",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SanPham_NhaCungCap",
                        column: x => x.MaNCC,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNCC",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    MaHD = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    MaKH = table.Column<string>(maxLength: 50, nullable: true),
                    NgayDat = table.Column<DateTime>(type: "timestamp", nullable: true),
                    NgayGiao = table.Column<DateTime>(type: "timestamp", nullable: true),
                    MaNV = table.Column<string>(maxLength: 50, nullable: true),
                    HoTen = table.Column<string>(maxLength: 50, nullable: true),
                    DiaChi = table.Column<string>(maxLength: 50, nullable: false),
                    CachThanhToan = table.Column<string>(maxLength: 50, nullable: true),
                    CachVanChuyen = table.Column<string>(maxLength: 50, nullable: true),
                    PhiVanChuyen = table.Column<double>(nullable: true),
                    MaTrangThai = table.Column<int>(nullable: true),
                    GhiChu = table.Column<string>(maxLength: 50, nullable: true),
                    DienThoai = table.Column<string>(maxLength: 50, nullable: true),
                    TenNgNhan = table.Column<string>(maxLength: 50, nullable: true),
                    DTNgNhan = table.Column<string>(maxLength: 50, nullable: true),
                    DiaChiNgNhan = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.MaHD);
                    table.ForeignKey(
                        name: "FK_HoaDon_KhachHang",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoaDon_NhanVien",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoaDon_TrangThai",
                        column: x => x.MaTrangThai,
                        principalTable: "TrangThai",
                        principalColumn: "MaTrangThai",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhanQuyen",
                columns: table => new
                {
                    MaPQ = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    MaPB = table.Column<string>(unicode: false, maxLength: 7, nullable: true),
                    MaTrang = table.Column<int>(nullable: true),
                    Them = table.Column<bool>(nullable: true),
                    Sua = table.Column<bool>(nullable: true),
                    Xoa = table.Column<bool>(nullable: true),
                    Xem = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanQuyen", x => x.MaPQ);
                    table.ForeignKey(
                        name: "FK_PhanQuyen_PhongBan",
                        column: x => x.MaPB,
                        principalTable: "PhongBan",
                        principalColumn: "MaPB",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhanQuyen_TrangWeb",
                        column: x => x.MaTrang,
                        principalTable: "TrangWeb",
                        principalColumn: "MaTrang",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BanBe",
                columns: table => new
                {
                    MaBB = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    MaKH = table.Column<string>(maxLength: 50, nullable: true),
                    MaSP = table.Column<int>(nullable: true),
                    HoTen = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    NgayGui = table.Column<DateTime>(type: "timestamp", nullable: true),
                    GhiChu = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanBe", x => x.MaBB);
                    table.ForeignKey(
                        name: "FK_BanBe_KhachHang",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BanBe_SanPham",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BinhLuan",
                columns: table => new
                {
                    MaBL = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    MaSP = table.Column<int>(nullable: true),
                    MaKH = table.Column<string>(maxLength: 50, nullable: true),
                    NgayBL = table.Column<DateTime>(type: "timestamp", nullable: false),
                    HoTen = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    NoiDung = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuan", x => x.MaBL);
                    table.ForeignKey(
                        name: "FK_BinhLuan_KhachHang",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BinhLuan_SanPham",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KhoHang",
                columns: table => new
                {
                    MaKho = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    MaSP = table.Column<int>(nullable: true),
                    SoLuong = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoHang", x => x.MaKho);
                    table.ForeignKey(
                        name: "FK_KhoHang_SanPham",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "YeuThich",
                columns: table => new
                {
                    MaYT = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    MaSP = table.Column<int>(nullable: true),
                    MaKH = table.Column<string>(maxLength: 50, nullable: true),
                    NgayChon = table.Column<DateTime>(type: "timestamp", nullable: true),
                    MoTa = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YeuThich", x => x.MaYT);
                    table.ForeignKey(
                        name: "FK_YeuThich_KhachHang",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YeuThich_SanPham",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHD",
                columns: table => new
                {
                    MaCT = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    MaHD = table.Column<int>(nullable: true),
                    MaSP = table.Column<int>(nullable: true),
                    DonGia = table.Column<double>(nullable: true),
                    SoLuong = table.Column<int>(nullable: true),
                    GiamGia = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHD", x => x.MaCT);
                    table.ForeignKey(
                        name: "FK_ChiTietHD_HoaDon",
                        column: x => x.MaHD,
                        principalTable: "HoaDon",
                        principalColumn: "MaHD",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChiTietHD_SanPham",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BanBe_MaKH",
                table: "BanBe",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_BanBe_MaSP",
                table: "BanBe",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuan_MaKH",
                table: "BinhLuan",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuan_MaSP",
                table: "BinhLuan",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_MaLoaiBlog",
                table: "Blog",
                column: "MaLoaiBlog");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHD_MaHD",
                table: "ChiTietHD",
                column: "MaHD");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHD_MaSP",
                table: "ChiTietHD",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_ChuDe_MaNV",
                table: "ChuDe",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaKH",
                table: "HoaDon",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaNV",
                table: "HoaDon",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaTrangThai",
                table: "HoaDon",
                column: "MaTrangThai");

            migrationBuilder.CreateIndex(
                name: "IX_HoiDap_MaNV",
                table: "HoiDap",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_KhoHang_MaSP",
                table: "KhoHang",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCong_MaNV",
                table: "PhanCong",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCong_MaPB",
                table: "PhanCong",
                column: "MaPB");

            migrationBuilder.CreateIndex(
                name: "IX_PhanQuyen_MaPB",
                table: "PhanQuyen",
                column: "MaPB");

            migrationBuilder.CreateIndex(
                name: "IX_PhanQuyen_MaTrang",
                table: "PhanQuyen",
                column: "MaTrang");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaHieu",
                table: "SanPham",
                column: "MaHieu");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaLoai",
                table: "SanPham",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaNCC",
                table: "SanPham",
                column: "MaNCC");

            migrationBuilder.CreateIndex(
                name: "IX_YeuThich_MaKH",
                table: "YeuThich",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_YeuThich_MaSP",
                table: "YeuThich",
                column: "MaSP");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BanBe");

            migrationBuilder.DropTable(
                name: "BinhLuan");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "ChiTietHD");

            migrationBuilder.DropTable(
                name: "ChuDe");

            migrationBuilder.DropTable(
                name: "HoiDap");

            migrationBuilder.DropTable(
                name: "KhoHang");

            migrationBuilder.DropTable(
                name: "LienHe");

            migrationBuilder.DropTable(
                name: "PhanCong");

            migrationBuilder.DropTable(
                name: "PhanQuyen");

            migrationBuilder.DropTable(
                name: "YeuThich");

            migrationBuilder.DropTable(
                name: "LoaiBlog");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "PhongBan");

            migrationBuilder.DropTable(
                name: "TrangWeb");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "TrangThai");

            migrationBuilder.DropTable(
                name: "ThuongHieu");

            migrationBuilder.DropTable(
                name: "Loai");

            migrationBuilder.DropTable(
                name: "NhaCungCap");
        }
    }
}
