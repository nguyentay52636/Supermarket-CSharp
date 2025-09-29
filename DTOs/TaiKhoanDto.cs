using System.ComponentModel.DataAnnotations;

namespace Supermarket.DTOs
{
    // TaiKhoan Management DTOs - Cho CRUD operations
    public class TaiKhoanDto
    {
        public int MaTaiKhoan { get; set; }
        public string TenNguoiDung { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public int? MaQuyen { get; set; }
        public string? TenQuyen { get; set; }
        public string TrangThai { get; set; } = string.Empty;
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }

    public class CreateTaiKhoanDto
    {
        [Required(ErrorMessage = "Tên người dùng là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên người dùng không được vượt quá 100 ký tự")]
        public string TenNguoiDung { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string SoDienThoai { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mã quyền là bắt buộc")]
        public int MaQuyen { get; set; } = 2; // Mặc định là khách hàng

        public string TrangThai { get; set; } = "Active";
    }

    public class UpdateTaiKhoanDto
    {
        [Required(ErrorMessage = "Tên người dùng là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên người dùng không được vượt quá 100 ký tự")]
        public string TenNguoiDung { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string SoDienThoai { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mã quyền là bắt buộc")]
        public int MaQuyen { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc")]
        public string TrangThai { get; set; } = string.Empty;
    }

    public class TaiKhoanListDto
    {
        public int MaTaiKhoan { get; set; }
        public string TenNguoiDung { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public int? MaQuyen { get; set; }
        public string? TenQuyen { get; set; }
        public string TrangThai { get; set; } = string.Empty;
        public DateTime? NgayTao { get; set; }
    }

    public class TaiKhoanSearchDto
    {
        public string? TenNguoiDung { get; set; }
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public int? MaQuyen { get; set; }
        public string? TrangThai { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "MaTaiKhoan";
        public string SortDirection { get; set; } = "asc";
    }

    public class TaiKhoanListResponseDto
    {
        public List<TaiKhoanListDto> Data { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }

    // Common DTOs
    public class UserInfoDto
    {
        public int MaTaiKhoan { get; set; }
        public string TenNguoiDung { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public int? MaQuyen { get; set; }
        public string? TenQuyen { get; set; }
        public string TrangThai { get; set; } = string.Empty;
    }

    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Mật khẩu mới là bắt buộc")]
        [MinLength(6, ErrorMessage = "Mật khẩu mới phải có ít nhất 6 ký tự")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Xác nhận mật khẩu mới là bắt buộc")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}