using System.ComponentModel.DataAnnotations;

namespace CESIZen.Shared.Models
{
    public class Category
    {
        [Key]
        public int id_category { get; set; }

        [Required]
        [MaxLength(50)]
        public string type { get; set; } = string.Empty;

        // Propriété de navigation
        public ICollection<Article>? Articles { get; set; }
    }
}
