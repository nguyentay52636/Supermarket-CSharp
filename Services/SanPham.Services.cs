using Microsoft.AspNetCore.Http;
using Supermarket.DTOs;
using Supermarket.Extensions;
using Supermarket.Models;
using Supermarket.Repositories.SanPhamRepositories;

namespace Supermarket.Services
{
    public interface ISanPhamService
    {
        Task<IEnumerable<SanPham>> GetAllAsync();
        Task<IEnumerable<SanPham>> SearchAsync(string? keyword, int? loaiId, int? thuongHieuId, decimal? minPrice, decimal? maxPrice);
        Task<SanPham?> GetByIdAsync(int id);
        Task<SanPhamDto> CreateAsync(CreateSanPhamDto dto, IFormFile? imageFile);
        Task<SanPhamDto?> UpdateAsync(int id, UpdateSanPhamDto dto, IFormFile? imageFile);
        Task<bool> DeleteAsync(int id);
    }

    public class SanPhamService : ISanPhamService
    {
        private readonly ISanPhamRepository _repository;
        private readonly IWebHostEnvironment _env;

        public SanPhamService(ISanPhamRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public Task<IEnumerable<SanPham>> GetAllAsync() => _repository.GetAllAsync();

        public Task<IEnumerable<SanPham>> SearchAsync(string? keyword, int? loaiId, int? thuongHieuId, decimal? minPrice, decimal? maxPrice)
            => _repository.SearchAsync(keyword, loaiId, thuongHieuId, minPrice, maxPrice);

        public Task<SanPham?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public async Task<SanPhamDto> CreateAsync(CreateSanPhamDto dto, IFormFile? imageFile)
        {
            var entity = new SanPham();
            entity.ApplyCreate(dto);

            if (imageFile != null)
            {
                entity.HinhAnh = await SaveImageAsync(imageFile);
            }

            var created = await _repository.AddAsync(entity);
            return created.ToDto();
        }

        public async Task<SanPhamDto?> UpdateAsync(int id, UpdateSanPhamDto dto, IFormFile? imageFile)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.ApplyUpdate(dto);
            if (imageFile != null)
            {
                existing.HinhAnh = await SaveImageAsync(imageFile);
            }

            var updated = await _repository.UpdateAsync(existing);
            return updated.ToDto();
        }

        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            var uploadsRootFolder = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads", "sanpham");
            Directory.CreateDirectory(uploadsRootFolder);

            var safeFileName = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsRootFolder, safeFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativePath = $"/uploads/sanpham/{safeFileName}";
            return relativePath;
        }
    }
}

