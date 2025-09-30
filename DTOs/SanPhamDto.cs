using System.ComponentModel.DataAnnotations;

namespace Supermarket.DTOs
{
    public class SanPhamDto
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public string? DonVi { get; set; }
        public int? SoLuongTon { get; set; }
        public int? MaThuongHieu { get; set; }
        public int? MaLoai { get; set; }
        public string? MoTa { get; set; }
        public decimal? GiaBan { get; set; }
        public string? HinhAnh { get; set; }
        public string? XuatXu { get; set; }
        public DateOnly? Hsd { get; set; }
        public string? TrangThai { get; set; }
    }

    public abstract class BaseSanPhamInputDto
    {
        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [MaxLength(255, ErrorMessage = "Tên sản phẩm tối đa 255 ký tự")]
        public string TenSanPham { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "Đơn vị tối đa 50 ký tự")]
        public string? DonVi { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn không hợp lệ")]
        public int? SoLuongTon { get; set; }

        public int? MaThuongHieu { get; set; }
        public int? MaLoai { get; set; }

        [MaxLength(2000, ErrorMessage = "Mô tả tối đa 2000 ký tự")]
        public string? MoTa { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá bán không hợp lệ")]
        public decimal? GiaBan { get; set; }

        [MaxLength(500, ErrorMessage = "Xuất xứ tối đa 500 ký tự")]
        public string? XuatXu { get; set; }

        public DateOnly? Hsd { get; set; }
    }

    public class CreateSanPhamDto : BaseSanPhamInputDto
    {
        public string? HinhAnh { get; set; }
    }

    public class UpdateSanPhamDto : BaseSanPhamInputDto
    {
        [Required]
        public int MaSanPham { get; set; }

        [MaxLength(1000, ErrorMessage = "Đường dẫn hình ảnh tối đa 1000 ký tự")]
        public string? HinhAnh { get; set; }

        public string? TrangThai { get; set; }
    }

    public class SanPhamListDto
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public decimal? GiaBan { get; set; }
        public string? HinhAnh { get; set; }
        public int? SoLuongTon { get; set; }
    }
}


