namespace ECommerce.API.DTOs.Cart
{
    public class CartItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
