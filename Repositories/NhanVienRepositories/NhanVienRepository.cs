using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Repositories.NhanVienRepositories
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

        public async Task<NhanVien?> GetByIdAsync(int id) =>
            await _context.NhanViens
                .Include(n => n.MaCuaHangNavigation)
                .FirstOrDefaultAsync(n => n.MaNhanVien == id);

        public async Task<NhanVien> AddAsync(NhanVien nhanVien)
        {
            _context.NhanViens.Add(nhanVien);
            await _context.SaveChangesAsync();
            return nhanVien;
        }

        public async Task<NhanVien> UpdateAsync(NhanVien nhanVien)
        {
            _context.NhanViens.Update(nhanVien);
            await _context.SaveChangesAsync();
            return nhanVien;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var nhanVien = await _context.NhanViens.FindAsync(id);
                if (nhanVien == null) return false;
                _context.NhanViens.Remove(nhanVien);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<NhanVien>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.NhanViens.ToListAsync();
            }

            var term = searchTerm.Trim();
            return await _context.NhanViens
                .Where(nv =>
                    (nv.TenNhanVien ?? "").Contains(term) ||
                    (nv.vaiTro ?? "").Contains(term) ||
                    (nv.SoDienThoai ?? "").Contains(term))
                .ToListAsync();
        }
    }
}
