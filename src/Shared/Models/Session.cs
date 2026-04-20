using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CESIZen.Shared.Models
{
    public class Session
    {
        [Key]
        public int id_session { get; set; }

        [MaxLength(255)]
        public string? token { get; set; }

        public DateTime? date_ { get; set; }

        [MaxLength(50)]
        public string? adress_ip { get; set; }

        public DateTime? expire_at { get; set; }

        [ForeignKey("User")]
        public int id_user { get; set; }

        // Propriété de navigation
        public User? User { get; set; }
    }
}
