using System.ComponentModel.DataAnnotations;

namespace CESIZen.Shared.Models
{
    public class Role
    {
        [Key]
        public int id_role { get; set; }

        [Required]
        [MaxLength(50)]
        public string type { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? description { get; set; }

        // Propriété de navigation
        public ICollection<User>? Users { get; set; }
    }
}
