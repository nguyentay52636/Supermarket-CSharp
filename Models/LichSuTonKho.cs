using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class LichSuTonKho
{
    public int MaLichSuTonKho { get; set; }

    public int? MaSanPham { get; set; }

    public int? MaCuaHang { get; set; }

    public int? SoLuongThayDoi { get; set; }

    public string? LyDo { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public virtual CuaHang? MaCuaHangNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
