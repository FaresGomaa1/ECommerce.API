using ECommerce.API.ECommerce.Domain.Model;

namespace ECommerce.API.DTOs.OrderDetails
{
    public class OrderDetailsAdd
    {
        public string Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}