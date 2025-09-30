using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supermarket.Models;

namespace Supermarket.Data.Configurations
{
    public class TaiKhoanConfiguration : IEntityTypeConfiguration<TaiKhoan>
    {
        public void Configure(EntityTypeBuilder<TaiKhoan> entity)
        {
            entity.HasKey(e => e.MaTaiKhoan);
            entity.ToTable("TaiKhoan");

            entity.HasOne(t => t.MaQuyenNavigation)
                .WithMany(p => p.TaiKhoans)
                .HasForeignKey(t => t.MaQuyen)
                .HasConstraintName("FK__TaiKhoan__MaQuye__6754599E")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}


