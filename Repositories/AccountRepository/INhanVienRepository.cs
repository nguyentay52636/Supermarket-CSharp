using Supermarket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supermarket.Repositories
{
    public interface INhanVienRepository
    {
        Task<IEnumerable<NhanVien>> GetAllAsync();
        Task<NhanVien?> GetByIdAsync(string id);
        Task<NhanVien> AddAsync(NhanVien nhanVien);
        Task<NhanVien> UpdateAsync(NhanVien nhanVien);
        Task<bool> DeleteAsync(string id);
    }
}
