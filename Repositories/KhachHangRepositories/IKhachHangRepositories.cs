using Supermarket.Models;

namespace Supermarket.Repositories.KhachHangRepositories
{
    public interface IKhachHangRepository
    {
        Task<IEnumerable<KhachHang>> GetAllAsync();
        Task<KhachHang?> GetByIdAsync(int id);
        Task<KhachHang> AddAsync(KhachHang khachHang);
        Task<KhachHang> UpdateAsync(KhachHang khachHang);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<KhachHang>> SearchAsync(string searchTerm);
    }
}
