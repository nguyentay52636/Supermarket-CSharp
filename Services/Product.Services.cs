/*
using Supermarket.Models;
using Supermarket.Repositories;

namespace Supermarket.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Product>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Product?> GetByIdAsync(string id) => _repository.GetByIdAsync(id);
        public Task<Product> AddAsync(Product product) => _repository.AddAsync(product);
        public Task<Product> UpdateAsync(Product product) => _repository.UpdateAsync(product);
        public Task<bool> DeleteAsync(string id) => _repository.DeleteAsync(id);
    }
}
*/
