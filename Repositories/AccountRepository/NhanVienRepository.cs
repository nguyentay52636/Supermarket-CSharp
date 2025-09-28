using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Repositories
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly ApplicationDbContext _context;

        public NhanVienRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NhanVien>> GetAllAsync() =>
            await _context.NhanViens.ToListAsync();

        public async Task<NhanVien?> GetByIdAsync(string id) =>
            await _context.NhanViens.FindAsync(id);

        public async Task<NhanVien> AddAsync(NhanVien nhanVien)
        {
            _context.NhanViens.Add(nhanVien);
            await _context.SaveChangesAsync();
            return nhanVien;
        }

        public async Task<NhanVien> UpdateAsync(NhanVien nhanVien)
        {
            nhanVien.UpdatedAt = DateTime.UtcNow;
            _context.NhanViens.Update(nhanVien);
            await _context.SaveChangesAsync();
            return nhanVien;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null) return false;
            _context.NhanViens.Remove(nhanVien);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
