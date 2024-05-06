namespace ECommerce.API.DTOs.OrderDetails
{
    public class OrderDetailsDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
