using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supermarket.Models;

namespace Supermarket.Data.Configurations
{
    public class KhachHangConfiguration : IEntityTypeConfiguration<KhachHang>
    {
        public void Configure(EntityTypeBuilder<KhachHang> entity)
        {
            entity.HasKey(e => e.MaKhachHang);
            entity.ToTable("KhachHang");
        }
    }
}


