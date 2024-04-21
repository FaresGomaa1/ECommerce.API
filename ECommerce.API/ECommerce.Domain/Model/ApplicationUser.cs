using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.ECommerce.Domain.Model
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)] 
        public string FullName { get; set; }
        [MaxLength(512)]
        public string Address { get; set; }

    }
}