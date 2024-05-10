using ECommerce.API.DTOs.OrderDetails;

namespace ECommerce.API.ECommerce.Application.Interfaces
{
    public interface IOrderDetailsRepository
    {
        Task<IEnumerable<OrderDetailsGetDTO>> GetOrderDetailsByOrderIdAsync(int orderId);
        Task AddOrderDetailsAsync(List<OrderDetailsAdd> orderDetailsList, int orderId);
        bool AreCombinationsAvailable(List<OrderDetailsAdd> orderDetailsList);
        Task<decimal> CalculateTotalAmountAsync(List<OrderDetailsAdd> orderDetailsList);
    }
}
