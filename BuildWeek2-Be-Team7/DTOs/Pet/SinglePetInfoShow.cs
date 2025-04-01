using BuildWeek2_Be_Team7.DTOs.Client;
using BuildWeek2_Be_Team7.DTOs.Hospitalization;
using BuildWeek2_Be_Team7.DTOs.MedicalExam;

namespace BuildWeek2_Be_Team7.DTOs.Pet
{
    public class SinglePetInfoShow
    {
        public Guid PetId { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public required string Name { get; set; }
        public required string Color { get; set; }
        public required string Race { get; set; }
        public required DateOnly BirthDate { get; set; }
        public string? Microchip { get; set; } = null;
        public OwnerDto? Owner { get; set; } = null!;
        public List<PetInfoShowExam>? PetExams { get; set;}
        public List<PetInfoShowHospital>? PetHospitalization { get; set; }
    }
}
