using ECommerce.API.DTOs.Cart;

namespace ECommerce.API.ECommerce.Application.Interfaces
{
    public interface ICartRepo
    {
        Task<CartGetDTO> GetCartAsync(int productId, string applicationUserId);
        Task<CartGetDTO> GetCartAsync(string userId);
        Task AddToCartAsync(CartAddEditDTO cartAddEditDTO);
        Task<(bool success, List<CartAddEditDTO> outOfStockItems)> UpdateCartAsync(List<CartAddEditDTO> newItems);
        Task<bool> RemoveFromCartAsync(int productId, string applicationUserId);
    }
}
