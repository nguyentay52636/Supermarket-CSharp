using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class DonHang
{
    public int MaDonHang { get; set; }

    public int? MaKhachHang { get; set; }

    public int? MaGioHang { get; set; }

    public DateTime? NgayDatHang { get; set; }

    public string? DiaChiGiaoHang { get; set; }

    public string? PhuongThucThanhToan { get; set; }

    public decimal? PhiVanChuyen { get; set; }

    public decimal? TongTien { get; set; }

    public string? TrangThai { get; set; }

    public int? MaHoaDon { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual ICollection<LichSuTichDiem> LichSuTichDiems { get; set; } = new List<LichSuTichDiem>();

    public virtual GioHang? MaGioHangNavigation { get; set; }

    public virtual HoaDon? MaHoaDonNavigation { get; set; }

    public virtual KhachHang? MaKhachHangNavigation { get; set; }
}
