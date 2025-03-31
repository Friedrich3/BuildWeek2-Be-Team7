using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BuildWeek2_Be_Team7.Models.Auth;

namespace BuildWeek2_Be_Team7.Models.Animali
{
    public class MedicalExam
    {
        [Key]
        public required Guid ExamId { get; set; }

        [Required]
        public DateTime ExamDate { get; set; }

        [Required]
        public required string Treatment {  get; set; }
        [Required]
        public required string Diagnosis { get; set; }

        [Required]
        public required Guid PetId { get; set; }
        [ForeignKey(nameof(PetId))]
        public Pet Pet { get; set; }

        [Required]
        [AllowedValues("Pending , Completed, Cancelled, NoShow")]
        public required string State { get; set; }

        [Required]
        public required string VetId { get; set; }
        [ForeignKey(nameof(VetId))]
        public ApplicationUser Vet { get; set; }
    } 
}
