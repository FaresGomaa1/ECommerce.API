using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.API.ECommerce.Domain.Model
{
    public class Cart
    {
        [Key, Column(Order = 3)]
        public string Size { get; set; }
        [Key, Column(Order = 4)]
        public string Color { get; set; }
        public int Quantity { get; set; }
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Key, Column(Order = 2)]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
