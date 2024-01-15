using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApi.Models
{
    public class Offer
    {
        [Key]
        public int Id { get; set; } 
        public string Title { get; set; } = string.Empty;
        public int Discount { get; set; }
        public virtual ICollection<Product> Products { get; set; } 
    }
}
