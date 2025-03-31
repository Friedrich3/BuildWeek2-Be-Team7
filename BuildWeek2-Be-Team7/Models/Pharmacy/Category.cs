using System.ComponentModel.DataAnnotations;

namespace BuildWeek2_Be_Team7.Models.Pharmacy
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
