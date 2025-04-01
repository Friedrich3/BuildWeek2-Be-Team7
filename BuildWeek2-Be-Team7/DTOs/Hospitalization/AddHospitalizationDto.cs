using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BuildWeek2_Be_Team7.DTOs.Pet;

namespace BuildWeek2_Be_Team7.DTOs.Hospitalization
{
    public class AddHospitalizationDto
    {
       
        [Required]
        public required DateOnly StartDate { get; set; }
        
        public required Guid PetId { get; set; }
       
    }
}
