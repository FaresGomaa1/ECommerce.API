using ECommerce.API.DTOs.Address;
using ECommerce.API.DTOs.Order;

namespace ECommerce.API.ECommerce.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync(string UserId);
        Task AddOrderAsync(OrderDTO order);
    }
}
