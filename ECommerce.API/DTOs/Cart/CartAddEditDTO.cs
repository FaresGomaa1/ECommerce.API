namespace ECommerce.API.DTOs.Cart
{
    public class CartAddEditDTO
    {
        public string Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
