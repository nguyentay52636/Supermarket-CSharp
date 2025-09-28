using System.ComponentModel.DataAnnotations;

namespace Supermarket.DTOs
{
    /// <summary>
    /// DTO cho thông tin nhân viên
    /// </summary>
    public class NhanVienDto
    {
        /// <summary>
        /// Mã nhân viên (GUID)
        /// </summary>
        public string MaNhanVien { get; set; } = string.Empty;

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string TenNhanVien { get; set; } = string.Empty;

        /// <summary>
        /// Giới tính
        /// </summary>
        public string GioiTinh { get; set; } = string.Empty;

        /// <summary>
        /// Ngày sinh (định dạng string)
        /// </summary>
        public string NgaySinh { get; set; } = string.Empty;

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string SoDienThoai { get; set; } = string.Empty;

        /// <summary>
        /// Vai trò của nhân viên
        /// </summary>
        public string VaiTro { get; set; } = string.Empty;

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Thời gian cập nhật cuối
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// DTO cho việc tạo nhân viên mới
    /// </summary>
    public class CreateNhanVienDto
    {
        /// <summary>
        /// Tên nhân viên (bắt buộc, tối đa 200 ký tự)
        /// </summary>
        [Required(ErrorMessage = "Tên nhân viên là bắt buộc")]
        [MaxLength(200, ErrorMessage = "Tên nhân viên không được vượt quá 200 ký tự")]
        public string TenNhanVien { get; set; } = string.Empty;

        /// <summary>
        /// Giới tính (bắt buộc)
        /// </summary>
        [Required(ErrorMessage = "Giới tính là bắt buộc")]
        public string GioiTinh { get; set; } = string.Empty;

        /// <summary>
        /// Ngày sinh (bắt buộc, định dạng string)
        /// </summary>
        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        public string NgaySinh { get; set; } = string.Empty;

        /// <summary>
        /// Số điện thoại (bắt buộc)
        /// </summary>
        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        public string SoDienThoai { get; set; } = string.Empty;

        /// <summary>
        /// Vai trò của nhân viên (bắt buộc)
        /// </summary>
        [Required(ErrorMessage = "Vai trò là bắt buộc")]
        public string VaiTro { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO cho việc cập nhật thông tin nhân viên
    /// </summary>
    public class UpdateNhanVienDto
    {
        /// <summary>
        /// Mã nhân viên (bắt buộc)
        /// </summary>
        [Required(ErrorMessage = "Mã nhân viên là bắt buộc")]
        public string MaNhanVien { get; set; } = string.Empty;

        /// <summary>
        /// Tên nhân viên (bắt buộc, tối đa 200 ký tự)
        /// </summary>
        [Required(ErrorMessage = "Tên nhân viên là bắt buộc")]
        [MaxLength(200, ErrorMessage = "Tên nhân viên không được vượt quá 200 ký tự")]
        public string TenNhanVien { get; set; } = string.Empty;

        /// <summary>
        /// Giới tính (bắt buộc)
        /// </summary>
        [Required(ErrorMessage = "Giới tính là bắt buộc")]
        public string GioiTinh { get; set; } = string.Empty;

        /// <summary>
        /// Ngày sinh (bắt buộc, định dạng string)
        /// </summary>
        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        public string NgaySinh { get; set; } = string.Empty;

        /// <summary>
        /// Số điện thoại (bắt buộc)
        /// </summary>
        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        public string SoDienThoai { get; set; } = string.Empty;

        /// <summary>
        /// Vai trò của nhân viên (bắt buộc)
        /// </summary>
        [Required(ErrorMessage = "Vai trò là bắt buộc")]
        public string VaiTro { get; set; } = string.Empty;
    }
}
