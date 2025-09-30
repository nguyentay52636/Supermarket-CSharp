using Supermarket.Models;

namespace Supermarket.Repositories.SanPhamRepositories
{
    public interface ISanPhamRepository
    {
        Task<IEnumerable<SanPham>> GetAllAsync();
        Task<SanPham?> GetByIdAsync(int id);
        Task<SanPham> AddAsync(SanPham entity);
        Task<SanPham> UpdateAsync(SanPham entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<SanPham>> SearchAsync(string? keyword, int? loaiId, int? thuongHieuId, decimal? minPrice, decimal? maxPrice);
    }
}


