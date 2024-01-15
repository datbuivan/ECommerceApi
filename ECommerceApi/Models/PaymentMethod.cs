using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApi.Models
{
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
        public string Available {  get; set; } = string.Empty;
        public string Reason {  get; set; } = string.Empty;

        public virtual ICollection<Payment> Payments { get; set; } 
    }
}
