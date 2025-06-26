using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EShop.Domain.Models;

public class Cart : BaseModel
{
    public int Id { get; set; }
    
    [Required]
    public int ClientId { get; set; }
    [JsonIgnore]
    public virtual Client Client { get; set; } = null!;
    
    [JsonIgnore]
    public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    
    [Range(0, double.MaxValue)]
    public decimal TotalAmount { get; set; }
    
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}

public class CartItem : BaseModel
{
    public int Id { get; set; }
    
    [Required]
    public int CartId { get; set; }
    [JsonIgnore]
    public virtual Cart Cart { get; set; } = null!;
    
    [Required]
    public int ProductId { get; set; }
    [JsonIgnore]
    public virtual Product Product { get; set; } = null!;
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal UnitPrice { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal TotalPrice { get; set; }
}