namespace ECommerce.API.DTOs.Cart
{
    public class CartGetDTO
    {
        public string ApplicationUserId { get; set; }
        public List<CartItemDTO> Items { get; set; }
    }
}
