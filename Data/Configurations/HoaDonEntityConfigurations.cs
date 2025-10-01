using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supermarket.Models;

namespace Supermarket.Data.Configurations
{
    public class HoaDonEntityConfigurations : IEntityTypeConfiguration<HoaDon>
    {
        public void Configure(EntityTypeBuilder<HoaDon> builder)
        {
            builder.HasKey(x => x.MaHoaDon);
            builder.Property(x => x.MaHoaDon).ValueGeneratedOnAdd();

            builder.Property(x => x.NgayLap).HasColumnType("datetime");
            builder.Property(x => x.PhuongThucThanhToan).HasMaxLength(100);
            builder.Property(x => x.TrangThai).HasMaxLength(50);

            builder.HasOne(x => x.MaNhanVienNavigation)
                .WithMany(p => p.HoaDons)
                .HasForeignKey(x => x.MaNhanVien)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.MaKhachHangNavigation)
                .WithMany(p => p.HoaDons)
                .HasForeignKey(x => x.MaKhachHang)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.MaCuaHangNavigation)
                .WithMany(p => p.HoaDons)
                .HasForeignKey(x => x.MaCuaHang)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}


