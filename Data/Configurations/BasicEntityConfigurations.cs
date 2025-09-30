using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supermarket.Models;

namespace Supermarket.Data.Configurations
{
    // Group simple key/table mappings to keep code concise
    public class ChiTietDonHangConfiguration : IEntityTypeConfiguration<ChiTietDonHang>
    {
        public void Configure(EntityTypeBuilder<ChiTietDonHang> entity)
        {
            entity.HasKey(e => e.MaChiTietDonHang);
            entity.ToTable("ChiTietDonHang");
        }
    }

    public class ChiTietGioHangConfiguration : IEntityTypeConfiguration<ChiTietGioHang>
    {
        public void Configure(EntityTypeBuilder<ChiTietGioHang> entity)
        {
            entity.HasKey(e => e.MaChiTietGioHang);
            entity.ToTable("ChiTietGioHang");
        }
    }

    public class ChiTietHoaDonConfiguration : IEntityTypeConfiguration<ChiTietHoaDon>
    {
        public void Configure(EntityTypeBuilder<ChiTietHoaDon> entity)
        {
            entity.HasKey(e => e.MaChiTietHoaDon);
            entity.ToTable("ChiTietHoaDon");
        }
    }

    public class ChiTietPhieuNhapConfiguration : IEntityTypeConfiguration<ChiTietPhieuNhap>
    {
        public void Configure(EntityTypeBuilder<ChiTietPhieuNhap> entity)
        {
            entity.HasKey(e => e.MaChiTietPhieuNhap);
            entity.ToTable("ChiTietPhieuNhap");
        }
    }

    public class CuaHangConfiguration : IEntityTypeConfiguration<CuaHang>
    {
        public void Configure(EntityTypeBuilder<CuaHang> entity)
        {
            entity.HasKey(e => e.MaCuaHang);
            entity.ToTable("CuaHang");
        }
    }

    public class DonHangConfiguration : IEntityTypeConfiguration<DonHang>
    {
        public void Configure(EntityTypeBuilder<DonHang> entity)
        {
            entity.HasKey(e => e.MaDonHang);
            entity.ToTable("DonHang");
        }
    }

    public class GioHangConfiguration : IEntityTypeConfiguration<GioHang>
    {
        public void Configure(EntityTypeBuilder<GioHang> entity)
        {
            entity.HasKey(e => e.MaGioHang);
            entity.ToTable("GioHang");
        }
    }

    public class HoaDonConfiguration : IEntityTypeConfiguration<HoaDon>
    {
        public void Configure(EntityTypeBuilder<HoaDon> entity)
        {
            entity.HasKey(e => e.MaHoaDon);
            entity.ToTable("HoaDon");
        }
    }

    public class KhuyenMaiConfiguration : IEntityTypeConfiguration<KhuyenMai>
    {
        public void Configure(EntityTypeBuilder<KhuyenMai> entity)
        {
            entity.HasKey(e => e.MaKhuyenMai);
            entity.ToTable("KhuyenMai");
        }
    }

    public class LichSuTichDiemConfiguration : IEntityTypeConfiguration<LichSuTichDiem>
    {
        public void Configure(EntityTypeBuilder<LichSuTichDiem> entity)
        {
            entity.HasKey(e => e.MaLichSuTichDiem);
            entity.ToTable("LichSuTichDiem");
        }
    }

    public class LichSuTonKhoConfiguration : IEntityTypeConfiguration<LichSuTonKho>
    {
        public void Configure(EntityTypeBuilder<LichSuTonKho> entity)
        {
            entity.HasKey(e => e.MaLichSuTonKho);
            entity.ToTable("LichSuTonKho");
        }
    }

    public class LoaiConfiguration : IEntityTypeConfiguration<Loai>
    {
        public void Configure(EntityTypeBuilder<Loai> entity)
        {
            entity.HasKey(e => e.MaLoai);
            entity.ToTable("Loai");
        }
    }

    public class MaGiamGiumConfiguration : IEntityTypeConfiguration<MaGiamGium>
    {
        public void Configure(EntityTypeBuilder<MaGiamGium> entity)
        {
            entity.HasKey(e => e.MaGiamGia);
            entity.ToTable("MaGiamGia");
        }
    }

    public class NhaCungCapConfiguration : IEntityTypeConfiguration<NhaCungCap>
    {
        public void Configure(EntityTypeBuilder<NhaCungCap> entity)
        {
            entity.HasKey(e => e.MaNhaCungCap);
            entity.ToTable("NhaCungCap");
        }
    }

    public class PhanQuyenConfiguration : IEntityTypeConfiguration<PhanQuyen>
    {
        public void Configure(EntityTypeBuilder<PhanQuyen> entity)
        {
            entity.HasKey(e => e.MaQuyen);
            entity.ToTable("PhanQuyen");
        }
    }

    public class PhieuNhapConfiguration : IEntityTypeConfiguration<PhieuNhap>
    {
        public void Configure(EntityTypeBuilder<PhieuNhap> entity)
        {
            entity.HasKey(e => e.MaPhieuNhap);
            entity.ToTable("PhieuNhap");
        }
    }

    public class SanPhamConfiguration : IEntityTypeConfiguration<SanPham>
    {
        public void Configure(EntityTypeBuilder<SanPham> entity)
        {
            entity.HasKey(e => e.MaSanPham);
            entity.ToTable("SanPham");

            // Configure relationships for foreign keys
            entity.HasOne(sp => sp.MaLoaiNavigation)
                .WithMany(l => l.SanPhams)
                .HasForeignKey(sp => sp.MaLoai)
                .HasConstraintName("FK_SanPham_Loai");

            entity.HasOne(sp => sp.MaThuongHieuNavigation)
                .WithMany(th => th.SanPhams)
                .HasForeignKey(sp => sp.MaThuongHieu)
                .HasConstraintName("FK_SanPham_ThuongHieu");
        }
    }

    public class ThuocTinhSanPhamConfiguration : IEntityTypeConfiguration<ThuocTinhSanPham>
    {
        public void Configure(EntityTypeBuilder<ThuocTinhSanPham> entity)
        {
            entity.HasKey(e => e.MaThuocTinh);
            entity.ToTable("ThuocTinhSanPham");
        }
    }

    public class ThuongHieuConfiguration : IEntityTypeConfiguration<ThuongHieu>
    {
        public void Configure(EntityTypeBuilder<ThuongHieu> entity)
        {
            entity.HasKey(e => e.MaThuongHieu);
            entity.ToTable("ThuongHieu");
        }
    }
}


