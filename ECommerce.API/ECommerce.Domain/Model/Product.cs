using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.API.ECommerce.Domain.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        [ForeignKey("Category")]
        public string CategoryName { get; set; }
        public Category Category { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
