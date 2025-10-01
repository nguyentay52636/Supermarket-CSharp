using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Repositories.HoaDonRepositories
{
    public class HoaDonRepository : IHoaDonRepository
    {
        private readonly ApplicationDbContext _db;

        public HoaDonRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<HoaDon>> GetAllAsync()
        {
            return await _db.HoaDons.AsNoTracking().ToListAsync();
        }

        public async Task<HoaDon?> GetByIdAsync(int id)
        {
            return await _db.HoaDons.FindAsync(id);
        }

        public async Task<HoaDon> AddAsync(HoaDon entity)
        {
            _db.HoaDons.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<HoaDon> UpdateAsync(HoaDon entity)
        {
            _db.HoaDons.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.HoaDons.FindAsync(id);
            if (existing == null) return false;
            _db.HoaDons.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}


