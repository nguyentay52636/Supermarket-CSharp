using Supermarket.Models;

namespace Supermarket.Repositories.AuthRepositories
{
    public interface IAuthRepositories
    {
        // Authentication methods
        Task<TaiKhoan?> GetTaiKhoanByEmailAsync(string email);
        Task<TaiKhoan?> GetTaiKhoanByPhoneAsync(string phone);
        Task<TaiKhoan?> GetTaiKhoanByIdAsync(int id);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<bool> CheckPhoneExistsAsync(string phone);

        // Password management
        Task<bool> UpdatePasswordAsync(int userId, string hashedPassword);

        // Token management (for future refresh token implementation)
        Task<string?> GetRefreshTokenAsync(int userId);
        Task<bool> SaveRefreshTokenAsync(int userId, string refreshToken);
        Task<bool> RevokeRefreshTokenAsync(int userId);
    }
}
