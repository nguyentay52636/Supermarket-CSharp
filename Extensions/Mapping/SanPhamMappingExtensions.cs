using Supermarket.DTOs;
using Supermarket.Models;

namespace Supermarket.Extensions
{
    public static class SanPhamMappingExtensions
    {
        public static SanPhamDto ToDto(this SanPham sp)
        {
            return new SanPhamDto
            {
                MaSanPham = sp.MaSanPham,
                TenSanPham = sp.TenSanPham ?? string.Empty,
                DonVi = sp.DonVi,
                SoLuongTon = sp.SoLuongTon,
                MaThuongHieu = sp.MaThuongHieu,
                MaLoai = sp.MaLoai,
                MoTa = sp.MoTa,
                GiaBan = sp.GiaBan,
                HinhAnh = sp.HinhAnh,
                XuatXu = sp.XuatXu,
                Hsd = sp.Hsd,
                TrangThai = sp.TrangThai
            };
        }

        public static SanPhamListDto ToListDto(this SanPham sp)
        {
            return new SanPhamListDto
            {
                MaSanPham = sp.MaSanPham,
                TenSanPham = sp.TenSanPham ?? string.Empty,
                GiaBan = sp.GiaBan,
                HinhAnh = sp.HinhAnh,
                SoLuongTon = sp.SoLuongTon
            };
        }

        public static IEnumerable<SanPhamDto> ToDtos(this IEnumerable<SanPham> items)
        {
            return items.Select(i => i.ToDto());
        }

        public static IEnumerable<SanPhamListDto> ToListDtos(this IEnumerable<SanPham> items)
        {
            return items.Select(i => i.ToListDto());
        }

        public static void ApplyCreate(this SanPham entity, CreateSanPhamDto dto)
        {
            entity.TenSanPham = dto.TenSanPham;
            entity.DonVi = dto.DonVi;
            entity.SoLuongTon = dto.SoLuongTon;
            entity.MaThuongHieu = dto.MaThuongHieu;
            entity.MaLoai = dto.MaLoai;
            entity.MoTa = dto.MoTa;
            entity.GiaBan = dto.GiaBan;
            entity.HinhAnh = dto.HinhAnh;
            entity.XuatXu = dto.XuatXu;
            entity.Hsd = dto.Hsd;
            entity.TrangThai = "Active";
        }

        public static void ApplyUpdate(this SanPham entity, UpdateSanPhamDto dto)
        {
            entity.TenSanPham = dto.TenSanPham;
            entity.DonVi = dto.DonVi;
            entity.SoLuongTon = dto.SoLuongTon;
            entity.MaThuongHieu = dto.MaThuongHieu;
            entity.MaLoai = dto.MaLoai;
            entity.MoTa = dto.MoTa;
            entity.GiaBan = dto.GiaBan;
            if (!string.IsNullOrWhiteSpace(dto.HinhAnh))
            {
                entity.HinhAnh = dto.HinhAnh;
            }
            entity.XuatXu = dto.XuatXu;
            entity.Hsd = dto.Hsd;
            entity.TrangThai = dto.TrangThai ?? entity.TrangThai;
        }
    }
}


