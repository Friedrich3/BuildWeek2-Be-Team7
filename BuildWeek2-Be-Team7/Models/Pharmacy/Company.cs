using System.ComponentModel.DataAnnotations;

namespace BuildWeek2_Be_Team7.Models.Pharmacy
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        [StringLength(10)]
        public required string Tel { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
