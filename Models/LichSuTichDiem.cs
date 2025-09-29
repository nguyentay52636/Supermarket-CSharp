using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class LichSuTichDiem
{
    public int MaLichSuTichDiem { get; set; }

    public int? MaKhachHang { get; set; }

    public int? MaHoaDon { get; set; }

    public int? MaDonHang { get; set; }

    public int? DiemCong { get; set; }

    public int? DiemSuDung { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public string? MoTa { get; set; }

    public virtual DonHang? MaDonHangNavigation { get; set; }

    public virtual HoaDon? MaHoaDonNavigation { get; set; }

    public virtual KhachHang? MaKhachHangNavigation { get; set; }
}
