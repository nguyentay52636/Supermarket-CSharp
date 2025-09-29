using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class KhachHang
{
    public int MaKhachHang { get; set; }

    public string? TenKhachHang { get; set; }

    public string? DiaChi { get; set; }

    public int? DiemTichLuy { get; set; }

    public string? HangThanhVien { get; set; }

    public int? MaTaiKhoan { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual ICollection<LichSuTichDiem> LichSuTichDiems { get; set; } = new List<LichSuTichDiem>();

    public virtual ICollection<MaGiamGium> MaGiamGia { get; set; } = new List<MaGiamGium>();

    public virtual TaiKhoan MaKhachHangNavigation { get; set; } = null!;
}
