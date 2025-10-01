using Supermarket.DTOs;
using Supermarket.Models;

namespace Supermarket.Extensions
{
    public static class HoaDonMappingExtensions
    {
        public static HoaDonDto ToDto(this HoaDon entity)
        {
            return new HoaDonDto
            {
                MaHoaDon = entity.MaHoaDon,
                NgayLap = entity.NgayLap ?? DateTime.UtcNow,
                MaNhanVien = entity.MaNhanVien ?? 0,
                MaKhachHang = entity.MaKhachHang ?? 0,
                MaCuaHang = entity.MaCuaHang ?? 0,
                PhuongThucThanhToan = entity.PhuongThucThanhToan ?? string.Empty,
                TongTien = entity.TongTien ?? 0,
                TrangThai = entity.TrangThai ?? string.Empty
            };
        }

        public static IEnumerable<HoaDonDto> ToDtos(this IEnumerable<HoaDon> items)
        {
            return items.Select(i => i.ToDto());
        }

        public static void ApplyCreate(this HoaDon entity, CreateHoaDonDto dto)
        {
            entity.NgayLap = dto.NgayLap == default ? DateTime.UtcNow : dto.NgayLap;
            entity.MaNhanVien = dto.MaNhanVien;
            entity.MaKhachHang = dto.MaKhachHang;
            entity.MaCuaHang = dto.MaCuaHang;
            entity.TrangThai = "Active";
        }

        public static void ApplyUpdate(this HoaDon entity, UpdateHoaDonDto dto)
        {
            entity.NgayLap = dto.NgayLap;
            entity.MaNhanVien = dto.MaNhanVien;
            entity.MaKhachHang = dto.MaKhachHang;
            entity.MaCuaHang = dto.MaCuaHang;
        }
    }
}


