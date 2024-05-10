using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.DTOs.Address
{
    public class AddressAddEditDTO
    {
        [StringLength(100, ErrorMessage = "Street length cannot exceed 100 characters")]
        public string Street { get; set; }
        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City length cannot exceed 50 characters")]
        public string City { get; set; }
        public int BlockNumber { get; set; }
        public int FloorLevel { get; set; }
    }
}
