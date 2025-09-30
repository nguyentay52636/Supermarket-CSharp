using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supermarket.Models;

namespace Supermarket.Data.Configurations
{
    public class NhanVienConfiguration : IEntityTypeConfiguration<NhanVien>
    {
        public void Configure(EntityTypeBuilder<NhanVien> entity)
        {
            entity.HasKey(e => e.MaNhanVien);
            entity.ToTable("NhanVien");

            entity.Property(n => n.TenNhanVien).IsRequired();
            entity.Property(n => n.GioiTinh).IsRequired();
            entity.Property(n => n.NgaySinh).IsRequired();
            entity.Property(n => n.SoDienThoai).IsRequired();
            entity.Property(n => n.vaiTro).IsRequired();
            entity.Property(n => n.TrangThai).IsRequired();

            entity.HasOne(n => n.MaCuaHangNavigation)
                .WithMany(c => c.NhanViens)
                .HasForeignKey(n => n.MaCuaHang)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}


