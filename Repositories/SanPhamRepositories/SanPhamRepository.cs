using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Repositories.SanPhamRepositories
{
    public class SanPhamRepository : ISanPhamRepository
    {
        private readonly ApplicationDbContext _db;

        public SanPhamRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<SanPham>> GetAllAsync()
        {
            return await _db.SanPhams.AsNoTracking().ToListAsync();
        }

        public async Task<SanPham?> GetByIdAsync(int id)
        {
            return await _db.SanPhams.FindAsync(id);
        }

        public async Task<SanPham> AddAsync(SanPham entity)
        {
            _db.SanPhams.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<SanPham> UpdateAsync(SanPham entity)
        {
            _db.SanPhams.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.SanPhams.FindAsync(id);
            if (existing == null) return false;
            _db.SanPhams.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SanPham>> SearchAsync(string? keyword, int? loaiId, int? thuongHieuId, decimal? minPrice, decimal? maxPrice)
        {
            var query = _db.SanPhams.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => (x.TenSanPham ?? "").Contains(keyword));
            }
            if (loaiId.HasValue)
            {
                query = query.Where(x => x.MaLoai == loaiId);
            }
            if (thuongHieuId.HasValue)
            {
                query = query.Where(x => x.MaThuongHieu == thuongHieuId);
            }
            if (minPrice.HasValue)
            {
                query = query.Where(x => x.GiaBan >= minPrice);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(x => x.GiaBan <= maxPrice);
            }

            return await query.ToListAsync();
        }
    }
}


