using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Repositories.TaiKhoanRepositories
{
    public class TaiKhoanRepositories : ITaiKhoanRepositories
    {
        private readonly ApplicationDbContext _context;

        public TaiKhoanRepositories(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaiKhoan?> GetTaiKhoanByEmailAsync(string email)
        {
            return await _context.TaiKhoans
                .Include(t => t.MaQuyenNavigation)
                .Include(t => t.NhanVien)
                .Include(t => t.KhachHang)
                .FirstOrDefaultAsync(t => t.Email == email);
        }

        public async Task<TaiKhoan?> GetTaiKhoanByPhoneAsync(string phone)
        {
            return await _context.TaiKhoans
                .Include(t => t.MaQuyenNavigation)
                .Include(t => t.NhanVien)
                .Include(t => t.KhachHang)
                .FirstOrDefaultAsync(t => t.SoDienThoai == phone);
        }

        public async Task<TaiKhoan?> GetTaiKhoanByIdAsync(int id)
        {
            return await _context.TaiKhoans
                .Include(t => t.MaQuyenNavigation)
                .Include(t => t.NhanVien)
                .Include(t => t.KhachHang)
                .FirstOrDefaultAsync(t => t.MaTaiKhoan == id);
        }

        public async Task<TaiKhoan> CreateTaiKhoanAsync(TaiKhoan taiKhoan)
        {
            _context.TaiKhoans.Add(taiKhoan);
            await _context.SaveChangesAsync();
            return taiKhoan;
        }

        public async Task<bool> UpdateTaiKhoanAsync(TaiKhoan taiKhoan)
        {
            try
            {
                _context.TaiKhoans.Update(taiKhoan);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteTaiKhoanAsync(int id)
        {
            try
            {
                var taiKhoan = await _context.TaiKhoans.FindAsync(id);
                if (taiKhoan == null) return false;

                _context.TaiKhoans.Remove(taiKhoan);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _context.TaiKhoans.AnyAsync(t => t.Email == email);
        }

        public async Task<bool> CheckPhoneExistsAsync(string phone)
        {
            return await _context.TaiKhoans.AnyAsync(t => t.SoDienThoai == phone);
        }
    }
}
