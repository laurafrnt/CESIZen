using CESIZen.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{

    public enum AcountStatut
    {
        Waiting = 0,
        Active = 1,
        Deactivated = 2,
        Deleted = 3
    }

    public class User
    {
        [Key] 
        public int id_user { get; set; }

        [Required]
        [MaxLength(150)]
        public string email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string password { get; set; } = string.Empty;
        public AcountStatut statut { get; set; } = AcountStatut.Waiting;

        [MaxLength(150)]
        public string? firstname { get; set; }

        [MaxLength(150)]
        public string? lastname { get; set; }

        public DateTime? birthday { get; set; }

        public DateTime created_at { get; set; } = DateTime.UtcNow;

        public DateTime? updated_at { get; set; }

        [ForeignKey("Role")]
        public int id_role { get; set; }

        // Navigation
        public Role? Role { get; set; }
        public Profile? Profile { get; set; }
        public ICollection<Article>? Articles { get; set; }
        public ICollection<Session>? Sessions { get; set; }
    }
}