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

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        public string? PostalCode { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Invalid password format")]
        public string Password { get; set; }
    }
}