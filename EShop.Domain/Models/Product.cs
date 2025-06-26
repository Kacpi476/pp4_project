using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EShop.Domain.Models
{
    public class Product : BaseModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; } = 0;
        
        public int? CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category? Category { get; set; }
    }
}
