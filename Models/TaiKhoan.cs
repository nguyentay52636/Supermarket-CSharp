using System;
using System.Collections.Generic;

namespace Supermarket.Models;

public partial class TaiKhoan
{
    public int MaTaiKhoan { get; set; }

    public string? TenNguoiDung { get; set; }

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public string? MatKhau { get; set; }

    public int? MaQuyen { get; set; }

    public string? TrangThai { get; set; }

    public virtual KhachHang? KhachHang { get; set; }

    public virtual PhanQuyen? MaQuyenNavigation { get; set; }

    public virtual NhanVien? NhanVien { get; set; }
}
