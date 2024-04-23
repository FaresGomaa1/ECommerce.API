using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.ECommerce.Domain.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
