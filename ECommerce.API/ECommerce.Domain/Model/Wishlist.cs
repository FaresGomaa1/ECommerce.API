using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.API.ECommerce.Domain.Model
{
    public class Wishlist
    {
        [Key, Column(Order = 1)]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Key, Column(Order = 2)]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
