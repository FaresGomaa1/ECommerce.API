using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.DTOs.Users
{
    public class UserLogInDTO
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
