using System.ComponentModel.DataAnnotations;

namespace Supermarket.DTOs
{
    public class NhanVienDto
    {
        public int MaNhanVien { get; set; }
        public string TenNhanVien { get; set; } = string.Empty;
        public string GioiTinh { get; set; } = string.Empty;
        public DateOnly? NgaySinh { get; set; }
        public string SoDienThoai { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string VaiTro { get; set; } = string.Empty;
        public int? MaCuaHang { get; set; }
        public string? TrangThai { get; set; }
    }

    public class CreateNhanVienDto
    {
        [Required(ErrorMessage = "Tên nhân viên là bắt buộc")]
        [MaxLength(200, ErrorMessage = "Tên nhân viên không được vượt quá 200 ký tự")]
        public string TenNhanVien { get; set; } = string.Empty;

        [Required(ErrorMessage = "Giới tính là bắt buộc")]
        public string GioiTinh { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        public DateOnly NgaySinh { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        public string SoDienThoai { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vai trò là bắt buộc")]
        public string VaiTro { get; set; } = string.Empty;

        public int? MaCuaHang { get; set; }
    }

    public class UpdateNhanVienDto
    {
        [Required(ErrorMessage = "Mã nhân viên là bắt buộc")]
        public int MaNhanVien { get; set; }

        [Required(ErrorMessage = "Tên nhân viên là bắt buộc")]
        [MaxLength(200, ErrorMessage = "Tên nhân viên không được vượt quá 200 ký tự")]
        public string TenNhanVien { get; set; } = string.Empty;

        [Required(ErrorMessage = "Giới tính là bắt buộc")]
        public string GioiTinh { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        public DateOnly NgaySinh { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        public string SoDienThoai { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vai trò là bắt buộc")]
        public string VaiTro { get; set; } = string.Empty;

        public int? MaCuaHang { get; set; }
        public string? TrangThai { get; set; }
    }
}
