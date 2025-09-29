using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class Loai
{
    public int MaLoai { get; set; }

    public string? TenLoai { get; set; }

    public string? MoTa { get; set; }

    public int? MaLoaiCha { get; set; }

    public virtual ICollection<Loai> InverseMaLoaiChaNavigation { get; set; } = new List<Loai>();

    public virtual ICollection<KhuyenMai> KhuyenMais { get; set; } = new List<KhuyenMai>();

    public virtual Loai? MaLoaiChaNavigation { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
