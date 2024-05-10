using ECommerce.API.DTOs.OrderDetails;

namespace ECommerce.API.DTOs.Order
{
    public class OrderDTO
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int AddressId { get; set; }
        public string OrderStatus { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
