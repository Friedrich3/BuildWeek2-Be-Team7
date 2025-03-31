using System.ComponentModel.DataAnnotations;
using BuildWeek2_Be_Team7.Models.Animali;
using BuildWeek2_Be_Team7.Models.Pharmacy;
using Microsoft.AspNetCore.Identity;

namespace BuildWeek2_Be_Team7.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public required DateOnly Birthdate { get; set; }
        [Required]
        [StringLength(16)]
        [RegularExpression(@"^[A-Z]{6}\d{2}[A-EHLMPRST]\d{2}[A-Z]\d{3}[A-Z]$")]
        public required string CodiceFiscale { get; set; }



        public ICollection<ApplicationUserRole> ApplicationUserRole { get; set; }
        public ICollection<Order>? Pharmacists { get; set; }
        public ICollection<Order>? Clients { get; set; }
        public ICollection<MedicalExam>? MedicalExams { get; set; }


    }
}
