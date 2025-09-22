using Supermarket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supermarket.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(string id);
        Task<Product> AddAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(string id);
    }
}
