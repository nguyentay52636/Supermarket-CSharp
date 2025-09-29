using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class ChiTietGioHang
{
    public int MaChiTietGioHang { get; set; }

    public int? MaGioHang { get; set; }

    public int? MaSanPham { get; set; }

    public int? SoLuong { get; set; }

    public decimal? GiaBanTaiThoiDiem { get; set; }

    public virtual GioHang? MaGioHangNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
