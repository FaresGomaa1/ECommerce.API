using ECommerce.API.DTOs.Product;
using ECommerce.API.ECommerce.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.API.Repositories
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<GetProductDTO>> GetAllProductsAsync();
        Task<IEnumerable<GetProductDTO>> GetSomeProductsAsync(int page, int pageSize);
        Task<GetProductDTO> GetProductByIdAsync(int id);
        Task UpdateProductAsync(Product product);
    }
}