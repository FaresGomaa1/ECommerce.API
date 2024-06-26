﻿

namespace ECommerce.API.ECommerce.Application.Interfaces
{
    public interface IGeneral
    {
        Task<IEnumerable<string>> GetAllCategoriesAsync();
        Task<IEnumerable<string>> GetAllSizesAsync();
        Task<IEnumerable<string>> GetAllColorssAsync();
        Task<int> GetProductCountAsync();
    }
}