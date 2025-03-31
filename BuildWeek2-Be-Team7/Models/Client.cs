using System.ComponentModel.DataAnnotations;
using BuildWeek2_Be_Team7.Models.Animali;
using BuildWeek2_Be_Team7.Models.Pharmacy;

namespace BuildWeek2_Be_Team7.Models
{
    public class Client
    {
        [Key]
        public Guid ClientId { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [StringLength(50)]
        public required string Surname { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public required DateOnly Birthdate { get; set; }
        [Required]
        [StringLength(16)]
        [RegularExpression(@"^[A-Z]{6}\d{2}[A-EHLMPRST]\d{2}[A-Z]\d{3}[A-Z]$")]
        public required string CodiceFiscale { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        public ICollection<Pet> Pets { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
