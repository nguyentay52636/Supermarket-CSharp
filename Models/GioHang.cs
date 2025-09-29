using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class GioHang
{
    public int MaGioHang { get; set; }

    public int? MaKhachHang { get; set; }

    public DateTime? NgayTao { get; set; }

    public decimal? TongTienTamTinh { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    public virtual KhachHang? MaKhachHangNavigation { get; set; }
}
