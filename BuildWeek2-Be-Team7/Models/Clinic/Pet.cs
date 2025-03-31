using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BuildWeek2_Be_Team7.Models.Auth;

namespace BuildWeek2_Be_Team7.Models.Animali
{
    public class Pet
    {
        [Key]
        public Guid PetId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly RegistrationDate { get; set; }

        [Required]
        [StringLength(25)]
        public required string Name { get; set; }

        [Required]
        [StringLength(20)]
        public required string Color { get; set; }

        [Required]
        public required int RaceId { get; set; }

        [ForeignKey(nameof(RaceId))]
        public required Race Race { get; set; }

        [Required]
        public required DateOnly BirthDate { get; set; }
        [Required]
        [StringLength(15)]
        public required string Microchip { get; set; }

        [Required]
        public required Guid OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public required Client Owner { get; set; }

        public string? Image {  get; set; }


        public ICollection<MedicalExam> MedicalExams { get; set; }

        public ICollection<Hospitalization> Hospitalizations { get; set; }
    }
}
