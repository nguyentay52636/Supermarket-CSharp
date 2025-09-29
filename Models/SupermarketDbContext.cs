using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Supermarket.Models;

public partial class SupermarketDbContext : DbContext
{
    public SupermarketDbContext()
    {
    }

    public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }

    public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    public virtual DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

    public virtual DbSet<CuaHang> CuaHangs { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<LichSuTichDiem> LichSuTichDiems { get; set; }

    public virtual DbSet<LichSuTonKho> LichSuTonKhos { get; set; }

    public virtual DbSet<Loai> Loais { get; set; }

    public virtual DbSet<MaGiamGium> MaGiamGia { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

    public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<ThuocTinhSanPham> ThuocTinhSanPhams { get; set; }

    public virtual DbSet<ThuongHieu> ThuongHieus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=supermarket;User Id=sa;Password=Tay52636@;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.MaChiTietDonHang).HasName("PK__ChiTietD__4B0B45DD4D06DADA");

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDonHang)
                .HasConstraintName("FK__ChiTietDo__MaDon__70DDC3D8");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__ChiTietDo__MaSan__71D1E811");
        });

        modelBuilder.Entity<ChiTietGioHang>(entity =>
        {
            entity.HasKey(e => e.MaChiTietGioHang).HasName("PK__ChiTietG__BBF474983C1E9CC4");

            entity.ToTable("ChiTietGioHang");

            entity.Property(e => e.GiaBanTaiThoiDiem).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaGioHangNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaGioHang)
                .HasConstraintName("FK__ChiTietGi__MaGio__73BA3083");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__ChiTietGi__MaSan__74AE54BC");
        });

        modelBuilder.Entity<ChiTietHoaDon>(entity =>
        {
            entity.HasKey(e => e.MaChiTietHoaDon).HasName("PK__ChiTietH__CFF2C426DE274F40");

            entity.ToTable("ChiTietHoaDon");

            entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaHoaDonNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.MaHoaDon)
                .HasConstraintName("FK__ChiTietHo__MaHoa__6C190EBB");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__ChiTietHo__MaSan__6D0D32F4");
        });

        modelBuilder.Entity<ChiTietPhieuNhap>(entity =>
        {
            entity.HasKey(e => e.MaChiTietPhieuNhap).HasName("PK__ChiTietP__8908D2839B0A46C4");

            entity.ToTable("ChiTietPhieuNhap");

            entity.Property(e => e.DonGiaNhap).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ThanhTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaPhieuNhapNavigation).WithMany(p => p.ChiTietPhieuNhaps)
                .HasForeignKey(d => d.MaPhieuNhap)
                .HasConstraintName("FK__ChiTietPh__MaPhi__7B5B524B");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietPhieuNhaps)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__ChiTietPh__MaSan__7A672E12");
        });

        modelBuilder.Entity<CuaHang>(entity =>
        {
            entity.HasKey(e => e.MaCuaHang).HasName("PK__CuaHang__0840BCA6264C5094");

            entity.ToTable("CuaHang");

            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenCuaHang).HasMaxLength(255);
            entity.Property(e => e.TrangThai).HasMaxLength(50);
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDonHang).HasName("PK__DonHang__129584ADA6BE314E");

            entity.ToTable("DonHang");

            entity.Property(e => e.DiaChiGiaoHang).HasMaxLength(255);
            entity.Property(e => e.NgayDatHang).HasColumnType("datetime");
            entity.Property(e => e.PhiVanChuyen).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PhuongThucThanhToan).HasMaxLength(50);
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaGioHangNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaGioHang)
                .HasConstraintName("FK__DonHang__MaGioHa__6EF57B66");

            entity.HasOne(d => d.MaHoaDonNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaHoaDon)
                .HasConstraintName("FK__DonHang__MaHoaDo__6FE99F9F");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__DonHang__MaKhach__6E01572D");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.MaGioHang).HasName("PK__GioHang__F5001DA3F03E999B");

            entity.ToTable("GioHang");

            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.TongTienTamTinh).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__GioHang__MaKhach__72C60C4A");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon).HasName("PK__HoaDon__835ED13BA674C67D");

            entity.ToTable("HoaDon");

            entity.Property(e => e.NgayLap).HasColumnType("datetime");
            entity.Property(e => e.PhuongThucThanhToan).HasMaxLength(50);
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaCuaHangNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaCuaHang)
                .HasConstraintName("FK__HoaDon__MaCuaHan__6477ECF3");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__HoaDon__MaKhachH__6B24EA82");

            entity.HasOne(d => d.MaNhanVienNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaNhanVien)
                .HasConstraintName("FK__HoaDon__MaNhanVi__6A30C649");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang).HasName("PK__KhachHan__88D2F0E57416F0FA");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKhachHang).ValueGeneratedOnAdd();
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.HangThanhVien).HasMaxLength(50);
            entity.Property(e => e.TenKhachHang).HasMaxLength(255);
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaKhachHangNavigation).WithOne(p => p.KhachHang)
                .HasForeignKey<KhachHang>(d => d.MaKhachHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KhachHang__MaKha__693CA210");
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaKhuyenMai).HasName("PK__KhuyenMa__6F56B3BD3870ACAA");

            entity.ToTable("KhuyenMai");

            entity.Property(e => e.LoaiKhuyenMai).HasMaxLength(50);
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");
            entity.Property(e => e.PhanTramGiamGia).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.SoTienGiam).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TenKhuyenMai).HasMaxLength(255);

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.KhuyenMais)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("FK__KhuyenMai__MaLoa__7D439ABD");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.KhuyenMais)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__KhuyenMai__MaSan__7C4F7684");

            entity.HasOne(d => d.MaThuongHieuNavigation).WithMany(p => p.KhuyenMais)
                .HasForeignKey(d => d.MaThuongHieu)
                .HasConstraintName("FK__KhuyenMai__MaThu__7E37BEF6");
        });

        modelBuilder.Entity<LichSuTichDiem>(entity =>
        {
            entity.HasKey(e => e.MaLichSuTichDiem).HasName("PK__LichSuTi__481DC56A55DD638B");

            entity.ToTable("LichSuTichDiem");

            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.LichSuTichDiems)
                .HasForeignKey(d => d.MaDonHang)
                .HasConstraintName("FK__LichSuTic__MaDon__778AC167");

            entity.HasOne(d => d.MaHoaDonNavigation).WithMany(p => p.LichSuTichDiems)
                .HasForeignKey(d => d.MaHoaDon)
                .HasConstraintName("FK__LichSuTic__MaHoa__76969D2E");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.LichSuTichDiems)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__LichSuTic__MaKha__75A278F5");
        });

        modelBuilder.Entity<LichSuTonKho>(entity =>
        {
            entity.HasKey(e => e.MaLichSuTonKho).HasName("PK__LichSuTo__E46DBF8DD40A96DC");

            entity.ToTable("LichSuTonKho");

            entity.Property(e => e.LyDo).HasMaxLength(255);
            entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");

            entity.HasOne(d => d.MaCuaHangNavigation).WithMany(p => p.LichSuTonKhos)
                .HasForeignKey(d => d.MaCuaHang)
                .HasConstraintName("FK__LichSuTon__MaCua__66603565");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.LichSuTonKhos)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__LichSuTon__MaSan__7F2BE32F");
        });

        modelBuilder.Entity<Loai>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PK__Loai__730A57591968D8C3");

            entity.ToTable("Loai");

            entity.Property(e => e.TenLoai).HasMaxLength(255);

            entity.HasOne(d => d.MaLoaiChaNavigation).WithMany(p => p.InverseMaLoaiChaNavigation)
                .HasForeignKey(d => d.MaLoaiCha)
                .HasConstraintName("FK__Loai__MaLoaiCha__619B8048");
        });

        modelBuilder.Entity<MaGiamGium>(entity =>
        {
            entity.HasKey(e => e.MaGiamGia).HasName("PK__MaGiamGi__EF9458E4B1300BF6");

            entity.Property(e => e.MaGiamGia).HasMaxLength(50);
            entity.Property(e => e.GiaTri).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Loai).HasMaxLength(50);
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.MaGiamGia)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__MaGiamGia__MaKha__787EE5A0");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNhaCungCap).HasName("PK__NhaCungC__53DA9205BA012689");

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenNhaCungCap).HasMaxLength(255);
            entity.Property(e => e.TrangThai).HasMaxLength(50);
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__NhanVien__77B2CA47B639D3F0");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNhanVien).ValueGeneratedOnAdd();
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenNhanVien).HasMaxLength(255);
            entity.Property(e => e.TrangThai).HasMaxLength(50);
            entity.Property(e => e.vaiTro).HasMaxLength(50);

            entity.HasOne(d => d.MaCuaHangNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.MaCuaHang)
                .HasConstraintName("FK__NhanVien__MaCuaH__6383C8BA");

            entity.HasOne(d => d.MaNhanVienNavigation).WithOne(p => p.NhanVien)
                .HasForeignKey<NhanVien>(d => d.MaNhanVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhanVien__MaNhan__68487DD7");
        });

        modelBuilder.Entity<PhanQuyen>(entity =>
        {
            entity.HasKey(e => e.MaQuyen).HasName("PK__PhanQuye__1D4B7ED4BF4CBB87");

            entity.ToTable("PhanQuyen");

            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenQuyen).HasMaxLength(100);
        });

        modelBuilder.Entity<PhieuNhap>(entity =>
        {
            entity.HasKey(e => e.MaPhieuNhap).HasName("PK__PhieuNha__1470EF3B5F54F4F4");

            entity.ToTable("PhieuNhap");

            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaCuaHangNavigation).WithMany(p => p.PhieuNhaps)
                .HasForeignKey(d => d.MaCuaHang)
                .HasConstraintName("FK__PhieuNhap__MaCua__656C112C");

            entity.HasOne(d => d.MaNhaCungCapNavigation).WithMany(p => p.PhieuNhaps)
                .HasForeignKey(d => d.MaNhaCungCap)
                .HasConstraintName("FK__PhieuNhap__MaNha__797309D9");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSanPham).HasName("PK__SanPham__FAC7442D8B3BB410");

            entity.ToTable("SanPham");

            entity.Property(e => e.DonVi).HasMaxLength(50);
            entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Hsd).HasColumnName("HSD");
            entity.Property(e => e.TenSanPham).HasMaxLength(255);
            entity.Property(e => e.TrangThai).HasMaxLength(50);
            entity.Property(e => e.XuatXu).HasMaxLength(100);

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("FK__SanPham__MaLoai__60A75C0F");

            entity.HasOne(d => d.MaThuongHieuNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaThuongHieu)
                .HasConstraintName("FK__SanPham__MaThuon__5FB337D6");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTaiKhoan).HasName("PK__TaiKhoan__AD7C6529052B65B6");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenNguoiDung)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaQuyenNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaQuyen)
                .HasConstraintName("FK__TaiKhoan__MaQuye__6754599E");
        });

        modelBuilder.Entity<ThuocTinhSanPham>(entity =>
        {
            entity.HasKey(e => e.MaThuocTinh).HasName("PK__ThuocTin__9EA5FC47AE60D50A");

            entity.ToTable("ThuocTinhSanPham");

            entity.Property(e => e.DonVi).HasMaxLength(50);
            entity.Property(e => e.GiaNhap).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GiaTri).HasMaxLength(100);
            entity.Property(e => e.TenThuocTinh).HasMaxLength(100);
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ThuocTinhSanPhams)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__ThuocTinh__MaSan__628FA481");
        });

        modelBuilder.Entity<ThuongHieu>(entity =>
        {
            entity.HasKey(e => e.MaThuongHieu).HasName("PK__ThuongHi__A3733E2C3406E5CB");

            entity.ToTable("ThuongHieu");

            entity.Property(e => e.TenThuongHieu).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
