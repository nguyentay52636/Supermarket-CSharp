using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class ThuongHieu
{
    public int MaThuongHieu { get; set; }

    public string? TenThuongHieu { get; set; }

    public virtual ICollection<KhuyenMai> KhuyenMais { get; set; } = new List<KhuyenMai>();

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
