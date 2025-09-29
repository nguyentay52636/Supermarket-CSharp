using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class NhanVien
{
    public int MaNhanVien { get; set; }

    public string? TenNhanVien { get; set; }

    public string? GioiTinh { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public string? vaiTro { get; set; }

    public int? MaCuaHang { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual CuaHang? MaCuaHangNavigation { get; set; }

    public virtual TaiKhoan? MaNhanVienNavigation { get; set; }
}
