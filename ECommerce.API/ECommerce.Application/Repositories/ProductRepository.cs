using AutoMapper;
using ECommerce.API.DTOs.Product;
using ECommerce.API.DTOs.ProductSize;
using ECommerce.API.ECommerce.Domain.Model;
using ECommerce.API.ECommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProductRepository(ECommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task AddProductAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(int id)
        {
            var productToDelete = await _dbContext.Products.FindAsync(id);
            if (productToDelete != null)
            {
                _dbContext.Products.Remove(productToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<GetProductDTO>> GetAllProductsAsync()
        {
            var products = await GetProductQuery().ToListAsync();
            return _mapper.Map<IEnumerable<GetProductDTO>>(products);
        }
        public async Task<IEnumerable<GetProductDTO>> GetSomeProductsAsync(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            var products = await GetProductQuery().Skip(skip).Take(pageSize).ToListAsync();
            return _mapper.Map<IEnumerable<GetProductDTO>>(products);
        }
        public async Task<GetProductDTO> GetProductByIdAsync(int id)
        {
            var product = await GetProductQuery().FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<GetProductDTO>(product);
        }
        public async Task UpdateProductAsync(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        private IQueryable<Product> GetProductQuery()
        {
            return _dbContext.Products
                              .Include(p => p.Photos)
                              .Include(p => p.ProductSizeColors.Where(ps => ps.Quantity > 0))
                              .Where(p => p.ProductSizeColors.Sum(psc => psc.Quantity) > 0);
        }
    }
}