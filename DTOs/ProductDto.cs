namespace Supermarket.DTOs
{
    public class ProductDto
    {
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string DonVi { get; set; }
        public int SoLuongTon { get; set; }
        public string MaThuongHieu { get; set; }
        public string MaDanhMuc { get; set; }
        public string MaLoai { get; set; }
        public string MoTa { get; set; }
        public decimal GiaBan { get; set; }
        public string HinhAnh { get; set; }
        public string XuatXu { get; set; }
        public DateTime HSD { get; set; }
        public string TrangThai { get; set; }
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }
        public string? TypeName { get; set; }
    }
}
