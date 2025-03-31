using BuildWeek2_Be_Team7.DTOs.Client;

namespace BuildWeek2_Be_Team7.DTOs.Pet
{
    public class PetMicrochip
    {
        public DateOnly RegistrationDate { get; set; }
        public required string Name { get; set; }
        public required string Color { get; set; }
        public required string Race { get; set; }
        public required DateOnly BirthDate { get; set; }
        public string Microchip { get; set; }
        public OwnerDto Owner { get; set; }
    }
}
