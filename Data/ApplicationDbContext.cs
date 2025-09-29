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
                    .WithMany()
                    .HasForeignKey(t => t.MaQuyen)
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
            });
        }
    }
}
