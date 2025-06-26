using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Models
{
    public class BaseModel
    {
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid UpdatedBy { get; set; }
    }
}
