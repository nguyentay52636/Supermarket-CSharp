using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class ThuocTinhSanPham
{
    public int MaThuocTinh { get; set; }

    public int? MaSanPham { get; set; }

    public string? TenThuocTinh { get; set; }

    public string? GiaTri { get; set; }

    public string? DonVi { get; set; }

    public decimal? GiaNhap { get; set; }

    public string? TrangThai { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
