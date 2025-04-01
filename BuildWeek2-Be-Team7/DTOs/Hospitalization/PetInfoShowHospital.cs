using System.ComponentModel.DataAnnotations;

namespace BuildWeek2_Be_Team7.DTOs.Hospitalization
{
    public class PetInfoShowHospital
    {
        public Guid HospitalizationId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
