using Microsoft.EntityFrameworkCore;
using Supermarket.Models;

namespace Supermarket.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<NhanVien> NhanViens { get; set; }

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
        }
    }
}
