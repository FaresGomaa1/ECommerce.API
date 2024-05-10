using ECommerce.API.ECommerce.Domain.Model;

namespace ECommerce.API.DTOs.OrderDetails
{
    public class OrderDetailsGetDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int OrderId { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}