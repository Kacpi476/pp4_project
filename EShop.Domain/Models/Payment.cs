using System.ComponentModel.DataAnnotations;
using EShop.Domain.Enums;
using System.Text.Json.Serialization;

namespace EShop.Domain.Models;

public class Payment : BaseModel
{
    public int Id { get; set; }
    
    [Required]
    public int OrderId { get; set; }
    [JsonIgnore]
    public virtual Order Order { get; set; } = null!;
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }
    
    [Required]
    public PaymentMethod PaymentMethod { get; set; }
    
    [Required]
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    
    [StringLength(255)]
    public string? TransactionId { get; set; }
    
    public DateTime? ProcessedAt { get; set; }
    
    [StringLength(500)]
    public string? Notes { get; set; }
}