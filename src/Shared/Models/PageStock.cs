using System.ComponentModel.DataAnnotations;

namespace CESIZen.Shared.Models
{
    public class PageStock
    {
        [Key]
        public int id_page_stock { get; set; }

        [Required]
        [MaxLength(50)]
        public string slug_url { get; set; } = string.Empty;

        [MaxLength(5000)]
        public string? html_content { get; set; }

        [Required]
        [MaxLength(20)]
        public string status { get; set; } = string.Empty;

        [Required]
        public bool is_system_page { get; set; }

        // Propriété de navigation
        public ICollection<Menu>? Menus { get; set; }
    }
}
