using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class PhieuNhap
{
    public int MaPhieuNhap { get; set; }

    public DateOnly? NgayNhap { get; set; }

    public int? MaNhaCungCap { get; set; }

    public int? MaCuaHang { get; set; }

    public decimal? TongTien { get; set; }

    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    public virtual CuaHang? MaCuaHangNavigation { get; set; }

    public virtual NhaCungCap? MaNhaCungCapNavigation { get; set; }
}
