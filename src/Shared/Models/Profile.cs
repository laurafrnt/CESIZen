using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CESIZen.Shared.Models;

namespace CESIZen.Shared.Models
{
    public class Profile
    {
        [Key]
        public int id_profile { get; set; }

        [Required]
        [MaxLength(50)]
        public string username { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? avatar { get; set; }

        public DateTime? last_connection { get; set; }

        [MaxLength(50)]
        public string? gender { get; set; }

        public DateTime created_at { get; set; } = DateTime.UtcNow;

        public DateTime? updated_at { get; set; }

        [ForeignKey("User")]
        public int id_user { get; set; }

        // Propriétés de navigation
        public User? User { get; set; }
        public ICollection<TrackerLog>? TrackerLogs { get; set; }
    }
}
