using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class KhuyenMai
{
    public int MaKhuyenMai { get; set; }

    public string? TenKhuyenMai { get; set; }

    public string? LoaiKhuyenMai { get; set; }

    public int? MaSanPham { get; set; }

    public int? MaLoai { get; set; }

    public int? MaThuongHieu { get; set; }

    public decimal? PhanTramGiamGia { get; set; }

    public decimal? SoTienGiam { get; set; }

    public string? DieuKienApDung { get; set; }

    public DateTime? NgayBatDau { get; set; }

    public DateTime? NgayKetThuc { get; set; }

    public string? MoTa { get; set; }

    public virtual Loai? MaLoaiNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }

    public virtual ThuongHieu? MaThuongHieuNavigation { get; set; }
}
