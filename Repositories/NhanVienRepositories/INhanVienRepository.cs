using Supermarket.Models;

namespace Supermarket.Repositories.NhanVienRepositories
{
    public interface INhanVienRepository
    {
        Task<IEnumerable<NhanVien>> GetAllAsync();
        Task<NhanVien?> GetByIdAsync(int id);
        Task<NhanVien> AddAsync(NhanVien nhanVien);
        Task<NhanVien> UpdateAsync(NhanVien nhanVien);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<NhanVien>> SearchAsync(string searchTerm);
    }
}
