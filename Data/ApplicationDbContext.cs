using Microsoft.EntityFrameworkCore;
using Supermarket.Models;

namespace Supermarket.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision for GiaBan
            modelBuilder.Entity<Product>()
                .Property(p => p.GiaBan)
                .HasPrecision(18, 2);

            // Configure string properties to avoid nullable warnings
            modelBuilder.Entity<Product>()
                .Property(p => p.TenSanPham)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.DonVi)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.MaThuongHieu)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.MaDanhMuc)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.MaLoai)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.MoTa)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.HinhAnh)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.XuatXu)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.TrangThai)
                .IsRequired();

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
        }
    }
}
