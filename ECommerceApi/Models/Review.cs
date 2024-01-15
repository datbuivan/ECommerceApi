using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApi.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string ReviewText { get; set; } = string.Empty;
        public string CreateAt { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } 
        [ForeignKey("ProductId")]
        public Product Product { get; set; } 
    }
}
