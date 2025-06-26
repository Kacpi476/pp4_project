using System.ComponentModel.DataAnnotations;
using EShop.Domain.Enums;
using System.Text.Json.Serialization;

namespace EShop.Domain.Models;

public class Order : BaseModel
{
    public int Id { get; set; }
    
    [Required]
    public int ClientId { get; set; }
    [JsonIgnore]
    public virtual Client Client { get; set; } = null!;
    
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    
    [Required]
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal TotalAmount { get; set; }
    
    [Required]
    public PaymentMethod PaymentMethod { get; set; }
    
    [Required]
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    
    [StringLength(500)]
    public string? Notes { get; set; }
    
    [JsonIgnore]
    public virtual Payment? Payment { get; set; }
    [JsonIgnore]
    public virtual Invoice? Invoice { get; set; }
}

public class OrderItem : BaseModel
{
    public int Id { get; set; }
    
    [Required]
    public int OrderId { get; set; }
    [JsonIgnore]
    public virtual Order Order { get; set; } = null!;
    
    [Required]
    public int ProductId { get; set; }
    [JsonIgnore]
    public virtual Product Product { get; set; } = null!;
    
    [Required]
    [StringLength(100)]
    public string ProductName { get; set; } = string.Empty;
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal ProductPrice { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal TotalPrice { get; set; }
}