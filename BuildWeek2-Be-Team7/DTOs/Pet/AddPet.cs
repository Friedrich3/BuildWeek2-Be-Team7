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
        public required string NameOwner { get; set; }
        public required string Surname { get; set; }
        public required DateOnly BirthdateOwner { get; set; }
        public required string CodiceFiscale { get; set; }
        public string Email { get; set; }
    }
}
