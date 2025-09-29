using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class ChiTietPhieuNhap
{
    public int MaChiTietPhieuNhap { get; set; }

    public int? MaSanPham { get; set; }

    public int? MaPhieuNhap { get; set; }

    public int? SoLuong { get; set; }

    public decimal? DonGiaNhap { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual PhieuNhap? MaPhieuNhapNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
