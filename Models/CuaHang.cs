using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class CuaHang
{
    public int MaCuaHang { get; set; }

    public string? TenCuaHang { get; set; }

    public string? DiaChi { get; set; }

    public string? SoDienThoai { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual ICollection<LichSuTonKho> LichSuTonKhos { get; set; } = new List<LichSuTonKho>();

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();

    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();
}
