using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.ECommerce.Domain.Model
{
    public class ProductColor
    {
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Key, Column(Order = 2)]
        [MaxLength(10)]
        public string ColorName { get; set; }
        public int Quantity { get; set; }
    }
}
