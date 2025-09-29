using Supermarket.Models;

namespace Supermarket.Repositories.TaiKhoanRepositories
{
    public interface ITaiKhoanRepositories
    {
        Task<TaiKhoan?> GetTaiKhoanByEmailAsync(string email);
        Task<TaiKhoan?> GetTaiKhoanByPhoneAsync(string phone);
        Task<TaiKhoan?> GetTaiKhoanByIdAsync(int id);
        Task<TaiKhoan> CreateTaiKhoanAsync(TaiKhoan taiKhoan);
        Task<bool> UpdateTaiKhoanAsync(TaiKhoan taiKhoan);
        Task<bool> DeleteTaiKhoanAsync(int id);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<bool> CheckPhoneExistsAsync(string phone);
    }
}