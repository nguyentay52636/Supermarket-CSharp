using System;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Product
    {
        [Key]
        public string MaSanPham { get; set; } = Guid.NewGuid().ToString();

        [Required, MaxLength(200)]
        public required string TenSanPham { get; set; }

        [MaxLength(50)]
        public required string DonVi { get; set; }

        public int SoLuongTon { get; set; }

        [Required]
        public required string MaThuongHieu { get; set; }

        [Required]
        public required string MaDanhMuc { get; set; }

        [Required]
        public required string MaLoai { get; set; }

        public required string MoTa { get; set; }

        [Range(0, double.MaxValue)]
        public decimal GiaBan { get; set; }

        public required string HinhAnh { get; set; }

        public required string XuatXu { get; set; }

        public DateTime HSD { get; set; }

        public required string TrangThai { get; set; }

        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }
        public string? TypeName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
