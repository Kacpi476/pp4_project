using System.ComponentModel.DataAnnotations;
using EShop.Domain.Enums;
using System.Text.Json.Serialization;

namespace EShop.Domain.Models;

public class Invoice : BaseModel
{
    public int Id { get; set; }
    
    [Required]
    public int OrderId { get; set; }
    [JsonIgnore]
    public virtual Order Order { get; set; } = null!;
    
    [Required]
    [StringLength(50)]
    public string InvoiceNumber { get; set; } = string.Empty;
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal TotalAmount { get; set; }
    
    [Required]
    public DateTime IssueDate { get; set; } = DateTime.UtcNow;
}