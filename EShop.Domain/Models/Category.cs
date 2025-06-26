using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EShop.Domain.Models
{
    public class Category : BaseModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
