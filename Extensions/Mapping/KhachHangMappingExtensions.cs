using Supermarket.DTOs;
using Supermarket.Models;

namespace Supermarket.Extensions
{
    public static class KhachHangMappingExtensions
    {
        public static KhachHangDto ToDto(this KhachHang kh)
        {
            return new KhachHangDto
            {
                MaKhachHang = kh.MaKhachHang,
                TenKhachHang = kh.TenKhachHang ?? string.Empty,
                DiaChi = kh.DiaChi,
                DiemTichLuy = kh.DiemTichLuy, 
                HangThanhVien = kh.HangThanhVien,
                MaTaiKhoan = kh.MaTaiKhoan,
                TrangThai = kh.TrangThai
            };
        }

        public static IEnumerable<KhachHangDto> ToDtos(this IEnumerable<KhachHang> khs)
        {
            return khs.Select(kh => kh.ToDto());
        }
    }
}

