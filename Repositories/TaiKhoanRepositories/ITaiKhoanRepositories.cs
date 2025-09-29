using Supermarket.Models;

namespace Supermarket.Repositories.TaiKhoanRepositories
{
    public interface ITaiKhoanRepositories
    {
        // CRUD Operations
        Task<TaiKhoan?> GetTaiKhoanByIdAsync(int id);
        Task<TaiKhoan> CreateTaiKhoanAsync(TaiKhoan taiKhoan);
        Task<bool> UpdateTaiKhoanAsync(TaiKhoan taiKhoan);
        Task<bool> DeleteTaiKhoanAsync(int id);

        // Basic list retrieval
        Task<List<TaiKhoan>> GetAllTaiKhoansAsync();

        // Search and Filter
        Task<List<TaiKhoan>> SearchTaiKhoansAsync(string? tenNguoiDung, string? email, string? soDienThoai, int? maQuyen, string? trangThai);

        // Validation
        Task<bool> CheckEmailExistsAsync(string email, int? excludeId = null);
        Task<bool> CheckPhoneExistsAsync(string phone, int? excludeId = null);

        // Status Management
        Task<bool> UpdateTaiKhoanStatusAsync(int id, string status);
        Task<bool> ResetPasswordAsync(int id, string hashedPassword);
    }
}
