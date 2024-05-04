using ECommerce.API.ECommerce.Application.Interfaces;
using ECommerce.API.ECommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.ECommerce.Application.Repositories
{
    public class GeneralRepo : IGeneral
    {
        private readonly ECommerceDbContext _dbContext;

        public GeneralRepo(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories
                .Select(c => c.CategoryName)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllSizesAsync()
        {
            return await _dbContext.Sizes
                .Select(s => s.SizeName)
                .ToListAsync();
        }

        public async Task<int> GetProductCountAsync()
        {
            return await _dbContext.Products
                .CountAsync();
        }
    }
}