using System.ComponentModel.DataAnnotations;

namespace Supermarket.DTOs
{
    public class KhachHangDto
    {
        public int MaKhachHang { get; set; }
        public string TenKhachHang { get; set; } = string.Empty;
        public string? DiaChi { get; set; }
        public int? DiemTichLuy { get; set; }
        public string? HangThanhVien { get; set; }
        public int? MaTaiKhoan { get; set; }
        public string? TrangThai { get; set; }
    }

    public class CreateKhachHangDto
    {
        [Required(ErrorMessage = "Tên khách hàng là bắt buộc")]
        [MaxLength(200, ErrorMessage = "Tên khách hàng không được vượt quá 200 ký tự")]
        public string TenKhachHang { get; set; } = string.Empty;

        public string? DiaChi { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Điểm tích lũy không hợp lệ")]
        public int? DiemTichLuy { get; set; }

        public string? HangThanhVien { get; set; }

        public int? MaTaiKhoan { get; set; }
    }

    public class UpdateKhachHangDto
    {
        [Required(ErrorMessage = "Mã khách hàng là bắt buộc")]
        public int MaKhachHang { get; set; }

        [Required(ErrorMessage = "Tên khách hàng là bắt buộc")]
        [MaxLength(200, ErrorMessage = "Tên khách hàng không được vượt quá 200 ký tự")]
        public string TenKhachHang { get; set; } = string.Empty;

        public string? DiaChi { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Điểm tích lũy không hợp lệ")]
        public int? DiemTichLuy { get; set; }

        public string? HangThanhVien { get; set; }

        public int? MaTaiKhoan { get; set; }
        public string? TrangThai { get; set; }
    }
}


