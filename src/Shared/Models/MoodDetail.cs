using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CESIZen.Shared.Models
{
    public class MoodDetail
    {
        [Key]
        public int id_mood_detail { get; set; }

        [Required]
        [MaxLength(50)]
        public string type { get; set; } = string.Empty;

        [ForeignKey("MoodPrimary")]
        public int id_mood_primary { get; set; }

        // Propriété de navigation
        public MoodPrimary? MoodPrimary { get; set; }
        public ICollection<TrackerLog>? TrackerLogs { get; set; }
    }
}
