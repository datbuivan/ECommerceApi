using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApi.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        public string Category { get; set; } = "";
        public string Subcategory { get; set; } = "";
        public virtual ICollection<Product> Products { get; set; } 
    }
}
