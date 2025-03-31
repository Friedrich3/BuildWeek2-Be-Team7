using System.ComponentModel.DataAnnotations;

namespace BuildWeek2_Be_Team7.Models.Pharmacy
{
    public class Drawer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Position { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
