using Supermarket.Models;

namespace Supermarket.Repositories.TaiKhoanRepositories
{
    public interface ITaiKhoanRepositories
    {
        // CRUD Operations
        Task<TaiKhoan?> GetTaiKhoanByIdAsync(int id);
        Task<List<TaiKhoan>> GetAllTaiKhoansAsync();
        Task<TaiKhoan> CreateTaiKhoanAsync(TaiKhoan taiKhoan);
        Task<bool> UpdateTaiKhoanAsync(TaiKhoan taiKhoan);
        Task<bool> DeleteTaiKhoanAsync(int id);

        // Search and Filter
        Task<List<TaiKhoan>> SearchTaiKhoansAsync(string? tenNguoiDung, string? email, string? soDienThoai, int? maQuyen, string? trangThai);
        Task<(List<TaiKhoan> Data, int TotalCount)> GetTaiKhoansPagedAsync(int page, int pageSize, string? tenNguoiDung, string? email, string? soDienThoai, int? maQuyen, string? trangThai, string sortBy, string sortDirection);

        // Validation
        Task<bool> CheckEmailExistsAsync(string email, int? excludeId = null);
        Task<bool> CheckPhoneExistsAsync(string phone, int? excludeId = null);

        // Status Management
        Task<bool> UpdateTaiKhoanStatusAsync(int id, string status);
        Task<bool> ResetPasswordAsync(int id, string hashedPassword);
    }
}
