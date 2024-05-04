using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.API.ECommerce.Domain.Model
{
    public class ProductSize
    {
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Key, Column(Order = 2)]
        [MaxLength(10)]
        public string SizeName { get; set; }
        public Size Size { get; set; }

        public int Quantity { get; set; }
    }
}
