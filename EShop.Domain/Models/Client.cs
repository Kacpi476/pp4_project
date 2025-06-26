using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EShop.Domain.Models;

public class Client : BaseModel
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;
    
    [Phone]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }
    
    [StringLength(255)]
    public string? Address { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    [JsonIgnore]
    public virtual Cart Cart { get; set; } = new Cart();
}