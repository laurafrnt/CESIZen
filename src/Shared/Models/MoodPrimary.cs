using System.ComponentModel.DataAnnotations;

namespace CESIZen.Shared.Models
{
    public class MoodPrimary
    {
        [Key]
        public int id_mood_primary { get; set; }

        [Required]
        [MaxLength(50)]
        public string titre { get; set; } = string.Empty;

        // Propriété de navigation
        public ICollection<MoodDetail>? MoodDetails { get; set; }
    }
}
