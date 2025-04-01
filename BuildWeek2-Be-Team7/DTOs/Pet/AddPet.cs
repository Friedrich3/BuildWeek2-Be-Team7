namespace BuildWeek2_Be_Team7.DTOs.Pet
{
    public class AddPet
    {
        public required string Name { get; set; }
        public required string Color { get; set; }
        public required int Race { get; set; }
        public required DateOnly BirthDate { get; set; }
        public string? Microchip { get; set; }

        //OWNER
        public string? NameOwner { get; set; } = null;
        public string? Surname { get; set; } = null;
        public DateOnly? BirthdateOwner { get; set; } = null;
        public string? CodiceFiscale { get; set; } = null;
        public string? Email { get; set; } = null;
    }
}
