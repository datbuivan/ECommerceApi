using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

namespace ECommerceApi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } =string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }   
        public int Quantity { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int OfferId { get; set; }
        [ForeignKey("OfferId")]
        public Offer Offer { get; set; }
        [ForeignKey("CategoryId")]
        public ProductCategory ProductCategory { get; set; } 
        public virtual ICollection<CartItems> CartItems { get; set; } 
    }
}
