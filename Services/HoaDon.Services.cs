using Supermarket.DTOs;
using Supermarket.Extensions;
using Supermarket.Models;
using Supermarket.Repositories.HoaDonRepositories;

namespace Supermarket.Services
{
    public interface IHoaDonService
    {
        Task<IEnumerable<HoaDon>> GetAllAsync();
        Task<HoaDon?> GetByIdAsync(int id);
        Task<HoaDonDto> CreateAsync(CreateHoaDonDto dto);
        Task<HoaDonDto?> UpdateAsync(int id, UpdateHoaDonDto dto);
        Task<bool> DeleteAsync(int id);
    }

    public class HoaDonService : IHoaDonService
    {
        private readonly IHoaDonRepository _repository;

        public HoaDonService(IHoaDonRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<HoaDon>> GetAllAsync() => _repository.GetAllAsync();

        public Task<HoaDon?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public async Task<HoaDonDto> CreateAsync(CreateHoaDonDto dto)
        {
            var entity = new HoaDon();
            entity.ApplyCreate(dto);

            var created = await _repository.AddAsync(entity);
            return created.ToDto();
        }

        public async Task<HoaDonDto?> UpdateAsync(int id, UpdateHoaDonDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.ApplyUpdate(dto);
            var updated = await _repository.UpdateAsync(existing);
            return updated.ToDto();
        }

        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}


