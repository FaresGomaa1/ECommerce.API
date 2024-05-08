using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.ECommerce.Domain.Model
{
    public class Color
    {
        [Key]
        [MaxLength(10)]
        public string ColorName { get; set; }
        public ICollection<ProductSizeColor> ProductSizeColors { get; set; }
    }
}
