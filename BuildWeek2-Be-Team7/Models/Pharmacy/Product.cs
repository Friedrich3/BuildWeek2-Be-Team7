using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildWeek2_Be_Team7.Models.Pharmacy
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required double Price { get; set; }

        [Required]
        public required bool isMed { get; set; }

        [Required]
        public required int IdCategory { get; set; }

        [Required]
        public required Guid IdCompany { get; set; }

        [Required]
        public required int IdDrawer { get; set; }

        [ForeignKey("IdCategory")]
        public Category Category { get; set; }

        [ForeignKey("IdCompany")]
        public Company Company { get; set; }

        [ForeignKey("IdDrawer")]
        public Drawer Drawer { get; set; }

        public ICollection<OrderProd> orderProds { get; set; }
    }
}
