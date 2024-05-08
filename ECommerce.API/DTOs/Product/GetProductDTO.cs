using ECommerce.API.DTOs.ProductSize;

namespace ECommerce.API.DTOs.Product
{
    public class GetProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string CategoryName { get; set; }
        public List<string> Photos { get; set; }
        public List<ProductSizeColorDTO> ProductSizeColors { get; set; }
    }
}
