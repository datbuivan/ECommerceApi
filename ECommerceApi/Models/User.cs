using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; }  = string.Empty;
        public string Email { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty;
        public string Mobile {  get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string CreateAt { get; set; } = string.Empty;
        public string ModifiedAt { get; set; } = string.Empty;  
        public virtual ICollection<Review> Reviews { get; set; } 
        public virtual ICollection<Payment> Payments { get; set; } 
        public virtual ICollection<Order> Orders { get; set; } 
        public virtual ICollection<Cart> Carts { get; set; } 
    }
}
