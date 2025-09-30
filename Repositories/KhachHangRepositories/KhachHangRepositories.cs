using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Repositories.KhachHangRepositories
{
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly ApplicationDbContext _db;

        public KhachHangRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<KhachHang> AddAsync(KhachHang khachHang)
        {
            _db.KhachHangs.Add(khachHang);
            await _db.SaveChangesAsync();
            return khachHang;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.KhachHangs.FindAsync(id);
            if (existing == null) return false;

            _db.KhachHangs.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<KhachHang>> GetAllAsync()
        {
            return await _db.KhachHangs.AsNoTracking().ToListAsync();
        }

        public async Task<KhachHang?> GetByIdAsync(int id)
        {
            return await _db.KhachHangs.AsNoTracking().FirstOrDefaultAsync(k => k.MaKhachHang == id);
        }

        public async Task<IEnumerable<KhachHang>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync();
            }

            var keyword = searchTerm.Trim().ToLower();
            return await _db.KhachHangs
                .AsNoTracking()
                .Where(k => (k.TenKhachHang ?? "").ToLower().Contains(keyword)
                         || (k.DiaChi ?? "").ToLower().Contains(keyword)
                         || (k.HangThanhVien ?? "").ToLower().Contains(keyword))
                .ToListAsync();
        }

        public async Task<KhachHang> UpdateAsync(KhachHang khachHang)
        {
            _db.KhachHangs.Update(khachHang);
            await _db.SaveChangesAsync();
            return khachHang;
        }
    }
}


