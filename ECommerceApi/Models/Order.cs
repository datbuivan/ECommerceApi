using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApi.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string CreateAt { get; set; } = string.Empty;
        public int UserId { get; set; } 
        public int CartId { get; set; }
        public int PaymentId { get; set; }
        public User User { get; set; } 
        [ForeignKey("CartId")]
        public Cart Cart { get; set; }
        public Payment Payment { get; set; } 
    }
}
