using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BuildWeek2_Be_Team7.DTOs.Pet;

namespace BuildWeek2_Be_Team7.DTOs.Hospitalization
{
    public class SingleHospitalizationDto
    {       
        public Guid HospitalizationId { get; set; }
        public required DateOnly StartDate { get; set; }
        public required PetHospitalizationDto Pet { get; set; }
    }
}
