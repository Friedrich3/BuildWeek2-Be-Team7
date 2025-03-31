using System.ComponentModel.DataAnnotations;

namespace BuildWeek2_Be_Team7.DTOs.Client
{
    public class Owner
    {
        public Guid IdOwner { get; set; }
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
    }
}
