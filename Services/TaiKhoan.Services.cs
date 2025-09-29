using Supermarket.DTOs;
using Supermarket.Models;
using Supermarket.Repositories.TaiKhoanRepositories;
using Supermarket.Extensions;
using System.Security.Cryptography;
using System.Text;

namespace Supermarket.Services
{
    public interface ITaiKhoanService
    {
        Task<TaiKhoanDto?> GetTaiKhoanByIdAsync(int id);
        Task<TaiKhoanDto> CreateTaiKhoanAsync(CreateTaiKhoanDto createDto);
        Task<TaiKhoanDto?> UpdateTaiKhoanAsync(int id, UpdateTaiKhoanDto updateDto);
        Task<bool> DeleteTaiKhoanAsync(int id);

        Task<bool> UpdateTaiKhoanStatusAsync(int id, string status);
        Task<bool> ResetPasswordAsync(int id, ResetPasswordDto resetDto);

        Task<IEnumerable<TaiKhoan>> GetAllTaiKhoansAsync();

    }

    public class TaiKhoanService : ITaiKhoanService
    {
        private readonly ITaiKhoanRepositories _repository;

        public TaiKhoanService(ITaiKhoanRepositories repository)
        {
            _repository = repository;
        }

        public async Task<TaiKhoanDto?> GetTaiKhoanByIdAsync(int id)
        {
            var taiKhoan = await _repository.GetTaiKhoanByIdAsync(id);
            if (taiKhoan == null) return null;

            return taiKhoan.ToDto();
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
            return createdTaiKhoan.ToDto();
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

            return taiKhoan.ToDto();
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

        public async Task<IEnumerable<TaiKhoan>> GetAllTaiKhoansAsync()
        {
            var list = await _repository.GetAllTaiKhoansAsync();
            return list;
        }


        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password ?? ""));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
