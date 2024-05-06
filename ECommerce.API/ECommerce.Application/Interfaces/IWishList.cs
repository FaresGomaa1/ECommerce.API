using ECommerce.API.DTOs.WishList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.API.ECommerce.Application.Interfaces
{
    public interface IWishList
    {
        Task<IEnumerable<WishListDTO>> GetAllItemsAsync(string UserId);
        Task<WishListDTO> GetItemByIdAsync(int itemId);
        Task AddItemAsync(WishListAddEditDTO item);
        Task DeleteItemAsync(int itemId,string UserId);
    }
}
