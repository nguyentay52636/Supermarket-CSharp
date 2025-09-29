CREATE TABLE [ThuongHieu] (
  [MaThuongHieu] INT PRIMARY KEY IDENTITY(1, 1),
  [TenThuongHieu] NVARCHAR(255)
)
GO

CREATE TABLE [Loai] (
  [MaLoai] INT PRIMARY KEY IDENTITY(1, 1),
  [TenLoai] NVARCHAR(255),
  [MoTa] NVARCHAR(MAX),
  [MaLoaiCha] INT
)
GO

CREATE TABLE [SanPham] (
  [MaSanPham] INT PRIMARY KEY IDENTITY(1, 1),
  [TenSanPham] NVARCHAR(255),
  [DonVi] NVARCHAR(50),
  [SoLuongTon] INT,
  [MaThuongHieu] INT,
  [MaLoai] INT,
  [MoTa] NVARCHAR(MAX),
  [GiaBan] DECIMAL(18,2),
  [HinhAnh] NVARCHAR(MAX),
  [XuatXu] NVARCHAR(100),
  [HSD] DATE,
  [TrangThai] NVARCHAR(50)
)
GO

CREATE TABLE [ThuocTinhSanPham] (
  [MaThuocTinh] INT PRIMARY KEY IDENTITY(1, 1),
  [MaSanPham] INT,
  [TenThuocTinh] NVARCHAR(100),
  [GiaTri] NVARCHAR(100),
  [DonVi] NVARCHAR(50),
  [GiaNhap] DECIMAL(18,2),
  [TrangThai] NVARCHAR(50)
)
GO

CREATE TABLE [CuaHang] (
  [MaCuaHang] INT PRIMARY KEY IDENTITY(1, 1),
  [TenCuaHang] NVARCHAR(255),
  [DiaChi] NVARCHAR(255),
  [SoDienThoai] VARCHAR(20),
  [TrangThai] NVARCHAR(50)
)
GO

CREATE TABLE [NhanVien] (
  [MaNhanVien] INT PRIMARY KEY IDENTITY(1, 1),
  [TenNhanVien] NVARCHAR(255),
  [GioiTinh] NVARCHAR(10),
  [NgaySinh] DATE,
  [SoDienThoai] VARCHAR(20),
  [Email] VARCHAR(255),
  [VaiTro] NVARCHAR(50),
  [MaCuaHang] INT,
  [TrangThai] NVARCHAR(50)
)
GO

CREATE TABLE [PhanQuyen] (
  [MaQuyen] INT PRIMARY KEY IDENTITY(1, 1),
  [TenQuyen] NVARCHAR(100),
  [MoTa] NVARCHAR(255)
)
GO

CREATE TABLE [TaiKhoan] (
  [MaTaiKhoan] INT PRIMARY KEY IDENTITY(1, 1),
  [TenNguoiDung] VARCHAR(255),
  [SoDienThoai] VARCHAR(20),
  [Email] VARCHAR(255),
  [MatKhau] VARCHAR(255),
  [MaQuyen] INT,
  [TrangThai] NVARCHAR(50)
)
GO

CREATE TABLE [KhachHang] (
  [MaKhachHang] INT PRIMARY KEY IDENTITY(1, 1),
  [TenKhachHang] NVARCHAR(255),
  [DiaChi] NVARCHAR(255),
  [DiemTichLuy] INT,
  [HangThanhVien] NVARCHAR(50),
  [MaTaiKhoan] INT,
  [TrangThai] NVARCHAR(50)
)
GO

CREATE TABLE [HoaDon] (
  [MaHoaDon] INT PRIMARY KEY IDENTITY(1, 1),
  [NgayLap] DATETIME,
  [MaNhanVien] INT,
  [MaKhachHang] INT,
  [MaCuaHang] INT,
  [PhuongThucThanhToan] NVARCHAR(50),
  [TongTien] DECIMAL(18,2),
  [TrangThai] NVARCHAR(50)
)
GO

CREATE TABLE [ChiTietHoaDon] (
  [MaChiTietHoaDon] INT PRIMARY KEY IDENTITY(1, 1),
  [MaHoaDon] INT,
  [MaSanPham] INT,
  [SoLuong] INT,
  [GiaBan] DECIMAL(18,2)
)
GO

CREATE TABLE [DonHang] (
  [MaDonHang] INT PRIMARY KEY IDENTITY(1, 1),
  [MaKhachHang] INT,
  [MaGioHang] INT,
  [NgayDatHang] DATETIME,
  [DiaChiGiaoHang] NVARCHAR(255),
  [PhuongThucThanhToan] NVARCHAR(50),
  [PhiVanChuyen] DECIMAL(18,2),
  [TongTien] DECIMAL(18,2),
  [TrangThai] NVARCHAR(50),
  [MaHoaDon] INT
)
GO

CREATE TABLE [ChiTietDonHang] (
  [MaChiTietDonHang] INT PRIMARY KEY IDENTITY(1, 1),
  [MaDonHang] INT,
  [MaSanPham] INT,
  [SoLuong] INT,
  [GiaBan] DECIMAL(18,2)
)
GO

CREATE TABLE [GioHang] (
  [MaGioHang] INT PRIMARY KEY IDENTITY(1, 1),
  [MaKhachHang] INT,
  [NgayTao] DATETIME,
  [TongTienTamTinh] DECIMAL(18,2),
  [TrangThai] NVARCHAR(50)
)
GO

CREATE TABLE [ChiTietGioHang] (
  [MaChiTietGioHang] INT PRIMARY KEY IDENTITY(1, 1),
  [MaGioHang] INT,
  [MaSanPham] INT,
  [SoLuong] INT,
  [GiaBanTaiThoiDiem] DECIMAL(18,2)
)
GO

CREATE TABLE [LichSuTichDiem] (
  [MaLichSuTichDiem] INT PRIMARY KEY IDENTITY(1, 1),
  [MaKhachHang] INT,
  [MaHoaDon] INT,
  [MaDonHang] INT,
  [DiemCong] INT,
  [DiemSuDung] INT,
  [NgayCapNhat] DATETIME,
  [MoTa] NVARCHAR(255)
)
GO

CREATE TABLE [MaGiamGia] (
  [MaGiamGia] NVARCHAR(50) PRIMARY KEY,
  [GiaTri] DECIMAL(18,2),
  [NgayHetHan] DATE,
  [MaKhachHang] INT,
  [Loai] NVARCHAR(50),
  [TrangThai] NVARCHAR(50)
)
GO

CREATE TABLE [NhaCungCap] (
  [MaNhaCungCap] INT PRIMARY KEY IDENTITY(1, 1),
  [TenNhaCungCap] NVARCHAR(255),
  [DiaChi] NVARCHAR(255),
  [SoDienThoai] VARCHAR(20),
  [Email] VARCHAR(255),
  [TrangThai] NVARCHAR(50)
)
GO

CREATE TABLE [PhieuNhap] (
  [MaPhieuNhap] INT PRIMARY KEY IDENTITY(1, 1),
  [NgayNhap] DATE,
  [MaNhaCungCap] INT,
  [MaCuaHang] INT,
  [TongTien] DECIMAL(18,2)
)
GO

CREATE TABLE [ChiTietPhieuNhap] (
  [MaChiTietPhieuNhap] INT PRIMARY KEY IDENTITY(1, 1),
  [MaSanPham] INT,
  [MaPhieuNhap] INT,
  [SoLuong] INT,
  [DonGiaNhap] DECIMAL(18,2),
  [ThanhTien] DECIMAL(18,2)
)
GO

CREATE TABLE [KhuyenMai] (
  [MaKhuyenMai] INT PRIMARY KEY IDENTITY(1, 1),
  [TenKhuyenMai] NVARCHAR(255),
  [LoaiKhuyenMai] NVARCHAR(50),
  [MaSanPham] INT,
  [MaLoai] INT,
  [MaThuongHieu] INT,
  [PhanTramGiamGia] DECIMAL(5,2),
  [SoTienGiam] DECIMAL(18,2),
  [DieuKienApDung] NVARCHAR(MAX),
  [NgayBatDau] DATETIME,
  [NgayKetThuc] DATETIME,
  [MoTa] NVARCHAR(MAX)
)
GO

CREATE TABLE [LichSuTonKho] (
  [MaLichSuTonKho] INT PRIMARY KEY IDENTITY(1, 1),
  [MaSanPham] INT,
  [MaCuaHang] INT,
  [SoLuongThayDoi] INT,
  [LyDo] NVARCHAR(255),
  [NgayCapNhat] DATETIME
)
GO

ALTER TABLE [SanPham] ADD FOREIGN KEY ([MaThuongHieu]) REFERENCES [ThuongHieu] ([MaThuongHieu])
GO

ALTER TABLE [SanPham] ADD FOREIGN KEY ([MaLoai]) REFERENCES [Loai] ([MaLoai])
GO

ALTER TABLE [Loai] ADD FOREIGN KEY ([MaLoaiCha]) REFERENCES [Loai] ([MaLoai])
GO

ALTER TABLE [ThuocTinhSanPham] ADD FOREIGN KEY ([MaSanPham]) REFERENCES [SanPham] ([MaSanPham])
GO

ALTER TABLE [NhanVien] ADD FOREIGN KEY ([MaCuaHang]) REFERENCES [CuaHang] ([MaCuaHang])
GO

ALTER TABLE [HoaDon] ADD FOREIGN KEY ([MaCuaHang]) REFERENCES [CuaHang] ([MaCuaHang])
GO

ALTER TABLE [PhieuNhap] ADD FOREIGN KEY ([MaCuaHang]) REFERENCES [CuaHang] ([MaCuaHang])
GO

ALTER TABLE [LichSuTonKho] ADD FOREIGN KEY ([MaCuaHang]) REFERENCES [CuaHang] ([MaCuaHang])
GO

ALTER TABLE [TaiKhoan] ADD FOREIGN KEY ([MaQuyen]) REFERENCES [PhanQuyen] ([MaQuyen])
GO

ALTER TABLE [NhanVien] ADD FOREIGN KEY ([MaNhanVien]) REFERENCES [TaiKhoan] ([MaTaiKhoan])
GO

ALTER TABLE [KhachHang] ADD FOREIGN KEY ([MaKhachHang]) REFERENCES [TaiKhoan] ([MaTaiKhoan])
GO

ALTER TABLE [HoaDon] ADD FOREIGN KEY ([MaNhanVien]) REFERENCES [NhanVien] ([MaNhanVien])
GO

ALTER TABLE [HoaDon] ADD FOREIGN KEY ([MaKhachHang]) REFERENCES [KhachHang] ([MaKhachHang])
GO

ALTER TABLE [ChiTietHoaDon] ADD FOREIGN KEY ([MaHoaDon]) REFERENCES [HoaDon] ([MaHoaDon])
GO

ALTER TABLE [ChiTietHoaDon] ADD FOREIGN KEY ([MaSanPham]) REFERENCES [SanPham] ([MaSanPham])
GO

ALTER TABLE [DonHang] ADD FOREIGN KEY ([MaKhachHang]) REFERENCES [KhachHang] ([MaKhachHang])
GO

ALTER TABLE [DonHang] ADD FOREIGN KEY ([MaGioHang]) REFERENCES [GioHang] ([MaGioHang])
GO

ALTER TABLE [DonHang] ADD FOREIGN KEY ([MaHoaDon]) REFERENCES [HoaDon] ([MaHoaDon])
GO

ALTER TABLE [ChiTietDonHang] ADD FOREIGN KEY ([MaDonHang]) REFERENCES [DonHang] ([MaDonHang])
GO

ALTER TABLE [ChiTietDonHang] ADD FOREIGN KEY ([MaSanPham]) REFERENCES [SanPham] ([MaSanPham])
GO

ALTER TABLE [GioHang] ADD FOREIGN KEY ([MaKhachHang]) REFERENCES [KhachHang] ([MaKhachHang])
GO

ALTER TABLE [ChiTietGioHang] ADD FOREIGN KEY ([MaGioHang]) REFERENCES [GioHang] ([MaGioHang])
GO

ALTER TABLE [ChiTietGioHang] ADD FOREIGN KEY ([MaSanPham]) REFERENCES [SanPham] ([MaSanPham])
GO

ALTER TABLE [LichSuTichDiem] ADD FOREIGN KEY ([MaKhachHang]) REFERENCES [KhachHang] ([MaKhachHang])
GO

ALTER TABLE [LichSuTichDiem] ADD FOREIGN KEY ([MaHoaDon]) REFERENCES [HoaDon] ([MaHoaDon])
GO

ALTER TABLE [LichSuTichDiem] ADD FOREIGN KEY ([MaDonHang]) REFERENCES [DonHang] ([MaDonHang])
GO

ALTER TABLE [MaGiamGia] ADD FOREIGN KEY ([MaKhachHang]) REFERENCES [KhachHang] ([MaKhachHang])
GO

ALTER TABLE [PhieuNhap] ADD FOREIGN KEY ([MaNhaCungCap]) REFERENCES [NhaCungCap] ([MaNhaCungCap])
GO

ALTER TABLE [ChiTietPhieuNhap] ADD FOREIGN KEY ([MaSanPham]) REFERENCES [SanPham] ([MaSanPham])
GO

ALTER TABLE [ChiTietPhieuNhap] ADD FOREIGN KEY ([MaPhieuNhap]) REFERENCES [PhieuNhap] ([MaPhieuNhap])
GO

ALTER TABLE [KhuyenMai] ADD FOREIGN KEY ([MaSanPham]) REFERENCES [SanPham] ([MaSanPham])
GO

ALTER TABLE [KhuyenMai] ADD FOREIGN KEY ([MaLoai]) REFERENCES [Loai] ([MaLoai])
GO

ALTER TABLE [KhuyenMai] ADD FOREIGN KEY ([MaThuongHieu]) REFERENCES [ThuongHieu] ([MaThuongHieu])
GO

ALTER TABLE [LichSuTonKho] ADD FOREIGN KEY ([MaSanPham]) REFERENCES [SanPham] ([MaSanPham])
GO
