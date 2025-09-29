using Supermarket.Models;
using Supermarket.Repositories.NhanVienRepositories;

namespace Supermarket.Services
{
    public class NhanVienService
    {
        private readonly INhanVienRepository _repository;

        public NhanVienService(INhanVienRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<NhanVien>> GetAllAsync() => _repository.GetAllAsync();
        public Task<NhanVien?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<NhanVien> AddAsync(NhanVien nhanVien) => _repository.AddAsync(nhanVien);
        public Task<NhanVien> UpdateAsync(NhanVien nhanVien) => _repository.UpdateAsync(nhanVien);
        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
