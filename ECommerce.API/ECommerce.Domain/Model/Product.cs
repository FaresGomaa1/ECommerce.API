using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.ECommerce.Domain.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
