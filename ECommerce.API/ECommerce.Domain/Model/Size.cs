using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.ECommerce.Domain.Model
{
    public class Size
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(10)]
        public string SizeName { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
    }
}
