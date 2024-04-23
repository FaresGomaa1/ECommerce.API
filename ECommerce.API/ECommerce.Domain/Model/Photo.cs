using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.ECommerce.Domain.Model
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
