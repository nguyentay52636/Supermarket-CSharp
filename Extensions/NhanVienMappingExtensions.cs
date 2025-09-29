using Supermarket.DTOs;
using Supermarket.Models;

namespace Supermarket.Extensions
{
    public static class NhanVienMappingExtensions
    {
        public static NhanVienDto ToDto(this NhanVien nv)
        {
            return new NhanVienDto
            {
                MaNhanVien = nv.MaNhanVien,
                TenNhanVien = nv.TenNhanVien ?? string.Empty,
                GioiTinh = nv.GioiTinh ?? string.Empty,
                NgaySinh = nv.NgaySinh,
                SoDienThoai = nv.SoDienThoai ?? string.Empty,
                Email = nv.Email,
                VaiTro = nv.vaiTro ?? string.Empty,
                MaCuaHang = nv.MaCuaHang,
                TrangThai = nv.TrangThai
            };
        }

        public static IEnumerable<NhanVienDto> ToDtos(this IEnumerable<NhanVien> nvs)
        {
            return nvs.Select(nv => nv.ToDto());
        }
    }
}


