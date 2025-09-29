using Supermarket.DTOs;
using Supermarket.Models;
using Supermarket.Repositories.TaiKhoanRepositories;
using System.Security.Cryptography;
using System.Text;

namespace Supermarket.Services
{
    public interface ITaiKhoanManagementService
    {
        // CRUD Operations
        Task<TaiKhoanDto?> GetTaiKhoanByIdAsync(int id);
        Task<TaiKhoanListResponseDto> GetAllTaiKhoansAsync(TaiKhoanSearchDto searchDto);
        Task<TaiKhoanDto> CreateTaiKhoanAsync(CreateTaiKhoanDto createDto);
        Task<TaiKhoanDto?> UpdateTaiKhoanAsync(int id, UpdateTaiKhoanDto updateDto);
        Task<bool> DeleteTaiKhoanAsync(int id);

        // Status Management
        Task<bool> UpdateTaiKhoanStatusAsync(int id, string status);
        Task<bool> ResetPasswordAsync(int id, ResetPasswordDto resetDto);

        // Validation
        Task<bool> CheckEmailExistsAsync(string email, int? excludeId = null);
        Task<bool> CheckPhoneExistsAsync(string phone, int? excludeId = null);
    }

    public class TaiKhoanManagementService : ITaiKhoanManagementService
    {
        private readonly ITaiKhoanManagementRepositories _repository;

        public TaiKhoanManagementService(ITaiKhoanManagementRepositories repository)
        {
            _repository = repository;
        }

        public async Task<TaiKhoanDto?> GetTaiKhoanByIdAsync(int id)
        {
            var taiKhoan = await _repository.GetTaiKhoanByIdAsync(id);
            if (taiKhoan == null) return null;

            return MapToDto(taiKhoan);
        }

        public async Task<TaiKhoanListResponseDto> GetAllTaiKhoansAsync(TaiKhoanSearchDto searchDto)
        {
            var (data, totalCount) = await _repository.GetTaiKhoansPagedAsync(
                searchDto.Page,
                searchDto.PageSize,
                searchDto.TenNguoiDung,
                searchDto.Email,
                searchDto.SoDienThoai,
                searchDto.MaQuyen,
                searchDto.TrangThai,
                searchDto.SortBy,
                searchDto.SortDirection
            );

            var totalPages = (int)Math.Ceiling((double)totalCount / searchDto.PageSize);

            return new TaiKhoanListResponseDto
            {
                Data = data.Select(MapToListDto).ToList(),
                TotalCount = totalCount,
                Page = searchDto.Page,
                PageSize = searchDto.PageSize,
                TotalPages = totalPages
            };
        }

        public async Task<TaiKhoanDto> CreateTaiKhoanAsync(CreateTaiKhoanDto createDto)
        {
            // Kiểm tra email đã tồn tại
            if (await _repository.CheckEmailExistsAsync(createDto.Email))
            {
                throw new InvalidOperationException("Email đã được sử dụng");
            }

            // Kiểm tra số điện thoại đã tồn tại
            if (await _repository.CheckPhoneExistsAsync(createDto.SoDienThoai))
            {
                throw new InvalidOperationException("Số điện thoại đã được sử dụng");
            }

            var taiKhoan = new TaiKhoan
            {
                TenNguoiDung = createDto.TenNguoiDung,
                Email = createDto.Email,
                SoDienThoai = createDto.SoDienThoai,
                MatKhau = HashPassword(createDto.Password),
                MaQuyen = createDto.MaQuyen,
                TrangThai = createDto.TrangThai
            };

            var createdTaiKhoan = await _repository.CreateTaiKhoanAsync(taiKhoan);
            return MapToDto(createdTaiKhoan);
        }

        public async Task<TaiKhoanDto?> UpdateTaiKhoanAsync(int id, UpdateTaiKhoanDto updateDto)
        {
            var taiKhoan = await _repository.GetTaiKhoanByIdAsync(id);
            if (taiKhoan == null) return null;

            // Kiểm tra email đã tồn tại (trừ tài khoản hiện tại)
            if (await _repository.CheckEmailExistsAsync(updateDto.Email, id))
            {
                throw new InvalidOperationException("Email đã được sử dụng");
            }

            // Kiểm tra số điện thoại đã tồn tại (trừ tài khoản hiện tại)
            if (await _repository.CheckPhoneExistsAsync(updateDto.SoDienThoai, id))
            {
                throw new InvalidOperationException("Số điện thoại đã được sử dụng");
            }

            taiKhoan.TenNguoiDung = updateDto.TenNguoiDung;
            taiKhoan.Email = updateDto.Email;
            taiKhoan.SoDienThoai = updateDto.SoDienThoai;
            taiKhoan.MaQuyen = updateDto.MaQuyen;
            taiKhoan.TrangThai = updateDto.TrangThai;

            var success = await _repository.UpdateTaiKhoanAsync(taiKhoan);
            if (!success) return null;

            return MapToDto(taiKhoan);
        }

        public async Task<bool> DeleteTaiKhoanAsync(int id)
        {
            return await _repository.DeleteTaiKhoanAsync(id);
        }

        public async Task<bool> UpdateTaiKhoanStatusAsync(int id, string status)
        {
            return await _repository.UpdateTaiKhoanStatusAsync(id, status);
        }

        public async Task<bool> ResetPasswordAsync(int id, ResetPasswordDto resetDto)
        {
            var hashedPassword = HashPassword(resetDto.NewPassword);
            return await _repository.ResetPasswordAsync(id, hashedPassword);
        }

        public async Task<bool> CheckEmailExistsAsync(string email, int? excludeId = null)
        {
            return await _repository.CheckEmailExistsAsync(email, excludeId);
        }

        public async Task<bool> CheckPhoneExistsAsync(string phone, int? excludeId = null)
        {
            return await _repository.CheckPhoneExistsAsync(phone, excludeId);
        }

        private TaiKhoanDto MapToDto(TaiKhoan taiKhoan)
        {
            return new TaiKhoanDto
            {
                MaTaiKhoan = taiKhoan.MaTaiKhoan,
                TenNguoiDung = taiKhoan.TenNguoiDung ?? "",
                Email = taiKhoan.Email ?? "",
                SoDienThoai = taiKhoan.SoDienThoai ?? "",
                MaQuyen = taiKhoan.MaQuyen,
                TenQuyen = taiKhoan.MaQuyenNavigation?.TenQuyen,
                TrangThai = taiKhoan.TrangThai ?? "",
                NgayTao = DateTime.Now, // TODO: Add NgayTao field to TaiKhoan model
                NgayCapNhat = DateTime.Now // TODO: Add NgayCapNhat field to TaiKhoan model
            };
        }

        private TaiKhoanListDto MapToListDto(TaiKhoan taiKhoan)
        {
            return new TaiKhoanListDto
            {
                MaTaiKhoan = taiKhoan.MaTaiKhoan,
                TenNguoiDung = taiKhoan.TenNguoiDung ?? "",
                Email = taiKhoan.Email ?? "",
                SoDienThoai = taiKhoan.SoDienThoai ?? "",
                MaQuyen = taiKhoan.MaQuyen,
                TenQuyen = taiKhoan.MaQuyenNavigation?.TenQuyen,
                TrangThai = taiKhoan.TrangThai ?? "",
                NgayTao = DateTime.Now // TODO: Add NgayTao field to TaiKhoan model
            };
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password ?? ""));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
