using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildWeek2_Be_Team7.Models.Animali
{
    public class Hospitalization
    {
        [Key]
        public Guid HospitalizationId { get; set; }
        [Required]
        public required DateOnly StartDate { get; set; }
        [Required]
        public DateOnly? EndDate { get; set; }
        [Required]
        public required Guid PetId { get; set; }
        [ForeignKey(nameof(PetId))]
        public Pet Pet { get; set; }



    }
}
