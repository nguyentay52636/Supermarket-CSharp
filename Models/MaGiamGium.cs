using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class MaGiamGium
{
    public string MaGiamGia { get; set; } = null!;

    public decimal? GiaTri { get; set; }

    public DateOnly? NgayHetHan { get; set; }

    public int? MaKhachHang { get; set; }

    public string? Loai { get; set; }

    public string? TrangThai { get; set; }

    public virtual KhachHang? MaKhachHangNavigation { get; set; }
}
