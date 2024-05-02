using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.DTOs
{
    public class MailRequestDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email must be a valid email address")]
        public string ToEmail { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Body is required")]
        public string Body { get; set; }
    }
}
