using Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CESIZen.Shared.Models
{
    public class Article
    {
        [Key]
        public int id_article { get; set; }

        [Required, MaxLength(150)]
        public string title { get; set; } = string.Empty;

        [Required]
        public string content { get; set; } = string.Empty;

        public string? imageUrl { get; set; }

        [Required]
        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        [Required]
        public int createdById { get; set; }

        public DateTime? lastUpdatedAt { get; set; }

        [Required]
        public int id_category { get; set; }

        // Navigation
        public Category? Category { get; set; }

        [ForeignKey("createdById")]
        public User? Author { get; set; }


    }
}