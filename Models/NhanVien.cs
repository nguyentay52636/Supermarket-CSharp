using System;
using System.ComponentModel.DataAnnotations;
namespace Supermarket.Models
{
    public class NhanVien
    {
        [Key]
        public string MaNhanVien { get; set; } = Guid.NewGuid().ToString();
        [Required, MaxLength(200)]
        public required string TenNhanVien { get; set; }
        [Required]
        public required string GioiTinh { get; set; }
        [Required]
        public required string NgaySinh { get; set; }
        [Required]
        public required string SoDienThoai { get; set; }
        [Required]
        public required string vaiTro { get; set; }
        public required string TrangThai { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

}