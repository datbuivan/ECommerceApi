using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApi.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public int TotalAmount { get; set; }
        public int ShippingCharges { get; set; }
        public int AmountReduced { get; set; }
        public int AmountPaid { get; set; }
        public string CreateAt { get; set; } = string.Empty; 
        public int UserId { get; set; }
        public int PaymentMethodId { get; set;}
        [ForeignKey("UserId")]
        public User User { get; set; } 
        [ForeignKey("PaymentMethodId")]
        public PaymentMethod PaymentMethod { get; set; } 
        public virtual ICollection<Order> Orders { get; set; } 
    }
}
