using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApi.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; } 
        public bool Ordered { get; set; } 
        public string OrdereOn { get; set; } = string.Empty;
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } 

        public virtual ICollection<CartItems> CartItems { get; set; } 
    }
}
