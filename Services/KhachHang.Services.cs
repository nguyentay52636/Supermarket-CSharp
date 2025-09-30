using Supermarket.Models;
using Supermarket.Repositories.KhachHangRepositories;

namespace Supermarket.Services
{
    public class KhachHangService
    {
        private readonly IKhachHangRepository _repository;

        public KhachHangService(IKhachHangRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<KhachHang>> GetAllAsync() => _repository.GetAllAsync();
        public Task<KhachHang?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<KhachHang> AddAsync(KhachHang khachHang) => _repository.AddAsync(khachHang);
        public Task<KhachHang> UpdateAsync(KhachHang khachHang) => _repository.UpdateAsync(khachHang);
        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
        public Task<IEnumerable<KhachHang>> SearchAsync(string searchTerm) => _repository.SearchAsync(searchTerm);
    }
}


