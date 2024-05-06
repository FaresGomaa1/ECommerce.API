namespace ECommerce.API.DTOs.WishList
{
    public class WishListDTO
    {
        public string ApplicationUserId { get; set; }
        public List<WishListItemDTO> Items { get; set; }
    }
}
