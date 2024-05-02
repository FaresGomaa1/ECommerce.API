using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.DTOs.Users
{
    public class UserLogInDTO
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
