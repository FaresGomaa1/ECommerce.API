using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.ECommerce.Domain.Model
{
    public class Size
    {
        [Key]
        [MaxLength(10)]
        public string SizeName { get; set; }
        public ICollection<ProductSizeColor> ProductSizeColors { get; set; }
    }
}
