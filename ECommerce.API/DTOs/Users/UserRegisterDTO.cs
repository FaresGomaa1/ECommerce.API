using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.DTOs.Users
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "Full name is required")]
        [RegularExpression(@"\b[a-zA-Z]{3,}\s[a-zA-Z]{3,}.+", ErrorMessage = "Full name must include at least your first name and last name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string? PostalCode { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Invalid password format")]
        public string Password { get; set; }
    }
}