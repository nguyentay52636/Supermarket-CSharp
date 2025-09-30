using System.ComponentModel.DataAnnotations;
namespace Supermarket.DTOs
{
    public class HoaDonDto
    {
        public int MaHoaDon { get; set; }
        public DateTime NgayLap { get; set; }
        public int MaNhanVien { get; set; }
        public int MaKhachHang { get; set; }
        public int MaCuaHang { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }
    }

    public class CreateHoaDonDto
    {
        public DateTime NgayLap { get; set; }
        public int MaNhanVien { get; set; }
        public int MaKhachHang { get; set; }
        public int MaCuaHang { get; set; }
    }

    public class UpdateHoaDonDto
    {
        public int MaHoaDon { get; set; }
        public DateTime NgayLap { get; set; }
        public int MaNhanVien { get; set; }
        public int MaKhachHang { get; set; }
        public int MaCuaHang { get; set; }
    }

    public class HoaDonResponseDto
    {
        public int MaHoaDon { get; set; }
        public DateTime NgayLap { get; set; }
        public int MaNhanVien { get; set; }
        public int MaKhachHang { get; set; }
        public int MaCuaHang { get; set; }
    }

    public class HoaDonDetailResponseDto
    {
        public int MaHoaDon { get; set; }
        public DateTime NgayLap { get; set; }
        public int MaNhanVien { get; set; }
        public int MaKhachHang { get; set; }
        public int MaCuaHang { get; set; }
    }
}
