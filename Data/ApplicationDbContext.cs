using Microsoft.EntityFrameworkCore;
using Supermarket.Models;

namespace Supermarket.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public DbSet<CuaHang> CuaHangs { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
        public DbSet<LichSuTichDiem> LichSuTichDiems { get; set; }
        public DbSet<LichSuTonKho> LichSuTonKhos { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<MaGiamGium> MaGiamGia { get; set; }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<PhanQuyen> PhanQuyens { get; set; }
        public DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<ThuocTinhSanPham> ThuocTinhSanPhams { get; set; }
        public DbSet<ThuongHieu> ThuongHieus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure NhanVien properties
            modelBuilder.Entity<NhanVien>()
                .Property(n => n.TenNhanVien)
                .IsRequired();

            modelBuilder.Entity<NhanVien>()
                .Property(n => n.GioiTinh)
                .IsRequired();

            modelBuilder.Entity<NhanVien>()
                .Property(n => n.NgaySinh)
                .IsRequired();

            modelBuilder.Entity<NhanVien>()
                .Property(n => n.SoDienThoai)
                .IsRequired();

            modelBuilder.Entity<NhanVien>()
                .Property(n => n.vaiTro)
                .IsRequired();

            modelBuilder.Entity<NhanVien>()
                .Property(n => n.TrangThai)
                .IsRequired();

            // Configure TaiKhoan relationships
            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.MaTaiKhoan);
                entity.ToTable("TaiKhoan");

                entity.HasOne(t => t.MaQuyenNavigation)
                    .WithMany(p => p.TaiKhoans)
                    .HasForeignKey(t => t.MaQuyen)
                    .HasConstraintName("FK__TaiKhoan__MaQuye__6754599E")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure KhachHang
            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKhachHang);
                entity.ToTable("KhachHang");
            });

            // Configure NhanVien
            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNhanVien);
                entity.ToTable("NhanVien");
                entity.HasOne(n => n.MaCuaHangNavigation)
                    .WithMany(c => c.NhanViens)
                    .HasForeignKey(n => n.MaCuaHang)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Explicit primary keys for remaining entities (conventions won't detect Vietnamese names)
            modelBuilder.Entity<ChiTietDonHang>(entity =>
            {
                entity.HasKey(e => e.MaChiTietDonHang);
                entity.ToTable("ChiTietDonHang");
            });

            modelBuilder.Entity<ChiTietGioHang>(entity =>
            {
                entity.HasKey(e => e.MaChiTietGioHang);
                entity.ToTable("ChiTietGioHang");
            });

            modelBuilder.Entity<ChiTietHoaDon>(entity =>
            {
                entity.HasKey(e => e.MaChiTietHoaDon);
                entity.ToTable("ChiTietHoaDon");
            });

            modelBuilder.Entity<ChiTietPhieuNhap>(entity =>
            {
                entity.HasKey(e => e.MaChiTietPhieuNhap);
                entity.ToTable("ChiTietPhieuNhap");
            });

            modelBuilder.Entity<CuaHang>(entity =>
            {
                entity.HasKey(e => e.MaCuaHang);
                entity.ToTable("CuaHang");
            });

            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.HasKey(e => e.MaDonHang);
                entity.ToTable("DonHang");
            });

            modelBuilder.Entity<GioHang>(entity =>
            {
                entity.HasKey(e => e.MaGioHang);
                entity.ToTable("GioHang");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHoaDon);
                entity.ToTable("HoaDon");
            });

            modelBuilder.Entity<KhuyenMai>(entity =>
            {
                entity.HasKey(e => e.MaKhuyenMai);
                entity.ToTable("KhuyenMai");
            });

            modelBuilder.Entity<LichSuTichDiem>(entity =>
            {
                entity.HasKey(e => e.MaLichSuTichDiem);
                entity.ToTable("LichSuTichDiem");
            });

            modelBuilder.Entity<LichSuTonKho>(entity =>
            {
                entity.HasKey(e => e.MaLichSuTonKho);
                entity.ToTable("LichSuTonKho");
            });

            modelBuilder.Entity<Loai>(entity =>
            {
                entity.HasKey(e => e.MaLoai);
                entity.ToTable("Loai");
            });

            modelBuilder.Entity<MaGiamGium>(entity =>
            {
                entity.HasKey(e => e.MaGiamGia);
                entity.ToTable("MaGiamGia");
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.HasKey(e => e.MaNhaCungCap);
                entity.ToTable("NhaCungCap");
            });

            modelBuilder.Entity<PhanQuyen>(entity =>
            {
                entity.HasKey(e => e.MaQuyen);
                entity.ToTable("PhanQuyen");
            });

            modelBuilder.Entity<PhieuNhap>(entity =>
            {
                entity.HasKey(e => e.MaPhieuNhap);
                entity.ToTable("PhieuNhap");
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSanPham);
                entity.ToTable("SanPham");
            });

            modelBuilder.Entity<ThuocTinhSanPham>(entity =>
            {
                entity.HasKey(e => e.MaThuocTinh);
                entity.ToTable("ThuocTinhSanPham");
            });

            modelBuilder.Entity<ThuongHieu>(entity =>
            {
                entity.HasKey(e => e.MaThuongHieu);
                entity.ToTable("ThuongHieu");
            });
        }
    }
}
