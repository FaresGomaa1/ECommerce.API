using ECommerce.API.DTOs.OrderDetails;

namespace ECommerce.API.ECommerce.Application.Interfaces
{
    public interface IOrderDetailsRepository
    {
        Task<IEnumerable<OrderDetailsDTO>> GetOrderDetailsByOrderIdAsync(int orderId);
        Task AddOrderDetailsAsync(OrderDetailsDTO orderDetails);
    }
}
