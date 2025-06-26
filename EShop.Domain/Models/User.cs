using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EShop.Domain.Models
{
    public class User : BaseModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string PasswordHash { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string? FirstName { get; set; }
        
        [StringLength(100)]
        public string? LastName { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        [JsonIgnore]
        public virtual Client? Client { get; set; }
        public int? ClientId { get; set; }
    }
}