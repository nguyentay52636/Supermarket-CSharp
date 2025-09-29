using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Repositories.AuthRepositories
{
    public class AuthRepositories : IAuthRepositories
    {
        private readonly ApplicationDbContext _context;

        public AuthRepositories(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaiKhoan?> GetTaiKhoanByEmailAsync(string email)
        {
            return await _context.TaiKhoans
                .Include(t => t.MaQuyenNavigation)
                .FirstOrDefaultAsync(t => t.Email == email);
        }

        public async Task<TaiKhoan?> GetTaiKhoanByPhoneAsync(string phone)
        {
            return await _context.TaiKhoans
                .Include(t => t.MaQuyenNavigation)
                .FirstOrDefaultAsync(t => t.SoDienThoai == phone);
        }

        public async Task<TaiKhoan?> GetTaiKhoanByIdAsync(int id)
        {
            return await _context.TaiKhoans
                .Include(t => t.MaQuyenNavigation)
                .FirstOrDefaultAsync(t => t.MaTaiKhoan == id);
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _context.TaiKhoans
                .AnyAsync(t => t.Email == email);
        }

        public async Task<bool> CheckPhoneExistsAsync(string phone)
        {
            return await _context.TaiKhoans
                .AnyAsync(t => t.SoDienThoai == phone);
        }

        public async Task<bool> UpdatePasswordAsync(int userId, string hashedPassword)
        {
            var taiKhoan = await _context.TaiKhoans.FindAsync(userId);
            if (taiKhoan == null) return false;

            taiKhoan.MatKhau = hashedPassword;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TaiKhoan> CreateTaiKhoanAsync(TaiKhoan taiKhoan)
        {
            _context.TaiKhoans.Add(taiKhoan);
            await _context.SaveChangesAsync();
            return taiKhoan;
        }

        // Token management methods (for future implementation)
        public async Task<string?> GetRefreshTokenAsync(int userId)
        {
            // TODO: Implement refresh token storage
            // This would require adding a RefreshToken table or field
            return await Task.FromResult<string?>(null);
        }

        public async Task<bool> SaveRefreshTokenAsync(int userId, string refreshToken)
        {
            // TODO: Implement refresh token storage
            return await Task.FromResult(true);
        }

        public async Task<bool> RevokeRefreshTokenAsync(int userId)
        {
            // TODO: Implement refresh token revocation
            return await Task.FromResult(true);
        }
    }
}
