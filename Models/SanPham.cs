using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class SanPham
{
    public int MaSanPham { get; set; }

    public string? TenSanPham { get; set; }

    public string? DonVi { get; set; }

    public int? SoLuongTon { get; set; }

    public int? MaThuongHieu { get; set; }

    public int? MaLoai { get; set; }

    public string? MoTa { get; set; }

    public decimal? GiaBan { get; set; }

    public string? HinhAnh { get; set; }

    public string? XuatXu { get; set; }

    public DateOnly? Hsd { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    public virtual ICollection<KhuyenMai> KhuyenMais { get; set; } = new List<KhuyenMai>();

    public virtual ICollection<LichSuTonKho> LichSuTonKhos { get; set; } = new List<LichSuTonKho>();

    public virtual Loai? MaLoaiNavigation { get; set; }

    public virtual ThuongHieu? MaThuongHieuNavigation { get; set; }

    public virtual ICollection<ThuocTinhSanPham> ThuocTinhSanPhams { get; set; } = new List<ThuocTinhSanPham>();
}
