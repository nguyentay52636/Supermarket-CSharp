using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class HoaDon
{
    public int MaHoaDon { get; set; }

    public DateTime? NgayLap { get; set; }

    public int? MaNhanVien { get; set; }

    public int? MaKhachHang { get; set; }

    public int? MaCuaHang { get; set; }

    public string? PhuongThucThanhToan { get; set; }

    public decimal? TongTien { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    public virtual ICollection<LichSuTichDiem> LichSuTichDiems { get; set; } = new List<LichSuTichDiem>();

    public virtual CuaHang? MaCuaHangNavigation { get; set; }

    public virtual KhachHang? MaKhachHangNavigation { get; set; }

    public virtual NhanVien? MaNhanVienNavigation { get; set; }
}
