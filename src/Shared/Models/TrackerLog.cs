using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CESIZen.Shared.Models
{
    public class TrackerLog
    {
        [Key]
        public int id_tracker_log { get; set; }

        public DateTime? date_ { get; set; }

        [MaxLength(50)]
        public string? comment { get; set; }

        [ForeignKey("Profile")]
        public int id_profile { get; set; }

        [ForeignKey("MoodDetail")]
        public int id_mood_detail { get; set; }

        // Propriétés de navigation
        public Profile? Profile { get; set; }
        public MoodDetail? MoodDetail { get; set; }
    }
}
