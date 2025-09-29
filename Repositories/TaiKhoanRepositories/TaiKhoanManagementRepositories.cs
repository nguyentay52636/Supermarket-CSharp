using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Repositories.TaiKhoanRepositories
{
    public class TaiKhoanManagementRepositories : ITaiKhoanManagementRepositories
    {
        private readonly ApplicationDbContext _context;

        public TaiKhoanManagementRepositories(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaiKhoan?> GetTaiKhoanByIdAsync(int id)
        {
            return await _context.TaiKhoans
                .Include(t => t.MaQuyenNavigation)
                .FirstOrDefaultAsync(t => t.MaTaiKhoan == id);
        }

        public async Task<List<TaiKhoan>> GetAllTaiKhoansAsync()
        {
            return await _context.TaiKhoans
                .Include(t => t.MaQuyenNavigation)
                .OrderBy(t => t.MaTaiKhoan)
                .ToListAsync();
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

        public async Task<List<TaiKhoan>> SearchTaiKhoansAsync(string? tenNguoiDung, string? email, string? soDienThoai, int? maQuyen, string? trangThai)
        {
            var query = _context.TaiKhoans.Include(t => t.MaQuyenNavigation).AsQueryable();

            if (!string.IsNullOrEmpty(tenNguoiDung))
                query = query.Where(t => t.TenNguoiDung!.Contains(tenNguoiDung));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(t => t.Email!.Contains(email));

            if (!string.IsNullOrEmpty(soDienThoai))
                query = query.Where(t => t.SoDienThoai!.Contains(soDienThoai));

            if (maQuyen.HasValue)
                query = query.Where(t => t.MaQuyen == maQuyen.Value);

            if (!string.IsNullOrEmpty(trangThai))
                query = query.Where(t => t.TrangThai == trangThai);

            return await query.OrderBy(t => t.MaTaiKhoan).ToListAsync();
        }

        public async Task<(List<TaiKhoan> Data, int TotalCount)> GetTaiKhoansPagedAsync(int page, int pageSize, string? tenNguoiDung, string? email, string? soDienThoai, int? maQuyen, string? trangThai, string sortBy, string sortDirection)
        {
            var query = _context.TaiKhoans.Include(t => t.MaQuyenNavigation).AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(tenNguoiDung))
                query = query.Where(t => t.TenNguoiDung!.Contains(tenNguoiDung));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(t => t.Email!.Contains(email));

            if (!string.IsNullOrEmpty(soDienThoai))
                query = query.Where(t => t.SoDienThoai!.Contains(soDienThoai));

            if (maQuyen.HasValue)
                query = query.Where(t => t.MaQuyen == maQuyen.Value);

            if (!string.IsNullOrEmpty(trangThai))
                query = query.Where(t => t.TrangThai == trangThai);

            // Get total count
            var totalCount = await query.CountAsync();

            // Apply sorting
            query = sortBy.ToLower() switch
            {
                "tennguoidung" => sortDirection.ToLower() == "desc" ? query.OrderByDescending(t => t.TenNguoiDung) : query.OrderBy(t => t.TenNguoiDung),
                "email" => sortDirection.ToLower() == "desc" ? query.OrderByDescending(t => t.Email) : query.OrderBy(t => t.Email),
                "sodienthoai" => sortDirection.ToLower() == "desc" ? query.OrderByDescending(t => t.SoDienThoai) : query.OrderBy(t => t.SoDienThoai),
                "maquyen" => sortDirection.ToLower() == "desc" ? query.OrderByDescending(t => t.MaQuyen) : query.OrderBy(t => t.MaQuyen),
                "trangthai" => sortDirection.ToLower() == "desc" ? query.OrderByDescending(t => t.TrangThai) : query.OrderBy(t => t.TrangThai),
                _ => sortDirection.ToLower() == "desc" ? query.OrderByDescending(t => t.MaTaiKhoan) : query.OrderBy(t => t.MaTaiKhoan)
            };

            // Apply paging
            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, totalCount);
        }

        public async Task<bool> CheckEmailExistsAsync(string email, int? excludeId = null)
        {
            var query = _context.TaiKhoans.Where(t => t.Email == email);

            if (excludeId.HasValue)
                query = query.Where(t => t.MaTaiKhoan != excludeId.Value);

            return await query.AnyAsync();
        }

        public async Task<bool> CheckPhoneExistsAsync(string phone, int? excludeId = null)
        {
            var query = _context.TaiKhoans.Where(t => t.SoDienThoai == phone);

            if (excludeId.HasValue)
                query = query.Where(t => t.MaTaiKhoan != excludeId.Value);

            return await query.AnyAsync();
        }

        public async Task<bool> UpdateTaiKhoanStatusAsync(int id, string status)
        {
            try
            {
                var taiKhoan = await _context.TaiKhoans.FindAsync(id);
                if (taiKhoan == null) return false;

                taiKhoan.TrangThai = status;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ResetPasswordAsync(int id, string hashedPassword)
        {
            try
            {
                var taiKhoan = await _context.TaiKhoans.FindAsync(id);
                if (taiKhoan == null) return false;

                taiKhoan.MatKhau = hashedPassword;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
