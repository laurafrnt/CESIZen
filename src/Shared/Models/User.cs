using CESIZen.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{
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

        [MaxLength(150)]
        public string? firstname { get; set; }

        [MaxLength(150)]
        public string? lastname { get; set; }

        public DateTime? birthday { get; set; }

        public DateTime created_at { get; set; } = DateTime.UtcNow;

        public DateTime? updated_at { get; set; }

        [ForeignKey("Role")]
        public int id_role { get; set; }

        public Role? Role { get; set; }
        public Profile? Profile { get; set; }
        public ICollection<Article>? Articles { get; set; }
        public ICollection<Session>? Sessions { get; set; }
    }
}