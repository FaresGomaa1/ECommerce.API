namespace ECommerce.API.DTOs.Cart
{
    public class CartAddEditDTO
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
