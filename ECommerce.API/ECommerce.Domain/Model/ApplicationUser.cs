using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.ECommerce.Domain.Model
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)] 
        public string FullName { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}