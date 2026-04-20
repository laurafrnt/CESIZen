using Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CESIZen.Shared.Models
{
    public class Article
    {
        [Key]
        public int id_article { get; set; }

        [Required]
        [MaxLength(50)]
        public string title { get; set; } = string.Empty;

        [MaxLength(5000)]
        public string? content { get; set; }

        public DateTime? creation_at { get; set; }

        [ForeignKey("Category")]
        public int id_category { get; set; }

        [ForeignKey("User")]
        public int id_user { get; set; }

        // Navigation properties
        public Category? Category { get; set; }
        public User? User { get; set; }
    }
}
