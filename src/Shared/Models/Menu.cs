using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CESIZen.Shared.Models
{
    public class Menu
    {
        [Key]
        [MaxLength(50)]
        public string id_menu { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string label { get; set; } = string.Empty;

        [Required]
        public int sort_order { get; set; }

        [ForeignKey("PageStock")]
        public int id_page_stock { get; set; }

        // Propriété de navigation
        public PageStock? PageStock { get; set; }
    }
}
