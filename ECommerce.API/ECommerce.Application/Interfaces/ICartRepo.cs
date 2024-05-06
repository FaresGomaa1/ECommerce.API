using ECommerce.API.DTOs.Cart;

namespace ECommerce.API.ECommerce.Application.Interfaces
{
    public interface ICartRepo
    {
        Task<CartGetDTO> GetCartAsync(int productId, string applicationUserId);
        Task<IEnumerable<CartGetDTO>> GetAllCartsAsync(string userId);
        Task AddToCartAsync(CartAddEditDTO cartAddEditDTO);
        Task<bool> UpdateCartAsync(CartAddEditDTO cartAddEditDTO);
        Task<bool> RemoveFromCartAsync(int productId, string applicationUserId);
    }
}
