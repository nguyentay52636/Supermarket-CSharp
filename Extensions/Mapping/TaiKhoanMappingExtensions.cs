using Supermarket.DTOs;
using Supermarket.Models;

namespace Supermarket.Extensions
{
    public static class TaiKhoanMappingExtensions
    {
        public static TaiKhoanDto ToDto(this TaiKhoan taiKhoan)
        {
            return new TaiKhoanDto
            {
                MaTaiKhoan = taiKhoan.MaTaiKhoan,
                TenNguoiDung = taiKhoan.TenNguoiDung ?? string.Empty,
                Email = taiKhoan.Email ?? string.Empty,
                SoDienThoai = taiKhoan.SoDienThoai ?? string.Empty,
                MaQuyen = taiKhoan.MaQuyen,
                TenQuyen = taiKhoan.MaQuyenNavigation?.TenQuyen,
                TrangThai = taiKhoan.TrangThai ?? string.Empty,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            };
        }

        public static TaiKhoanListDto ToListDto(this TaiKhoan taiKhoan)
        {
            return new TaiKhoanListDto
            {
                MaTaiKhoan = taiKhoan.MaTaiKhoan,
                TenNguoiDung = taiKhoan.TenNguoiDung ?? string.Empty,
                Email = taiKhoan.Email ?? string.Empty,
                SoDienThoai = taiKhoan.SoDienThoai ?? string.Empty,
                MaQuyen = taiKhoan.MaQuyen,
                TenQuyen = taiKhoan.MaQuyenNavigation?.TenQuyen,
                TrangThai = taiKhoan.TrangThai ?? string.Empty,
                NgayTao = DateTime.Now
            };
        }
    }
}


