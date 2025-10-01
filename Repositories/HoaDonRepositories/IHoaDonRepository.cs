using Supermarket.Models;

namespace Supermarket.Repositories.HoaDonRepositories
{
    public interface IHoaDonRepository
    {
        Task<IEnumerable<HoaDon>> GetAllAsync();
        Task<HoaDon?> GetByIdAsync(int id);
        Task<HoaDon> AddAsync(HoaDon entity);
        Task<HoaDon> UpdateAsync(HoaDon entity);
        Task<bool> DeleteAsync(int id);
    }
}



