namespace BuildWeek2_Be_Team7.DTOs.Pet
{
    public class EditPet
    {
        public required string Name { get; set; }
        public required string Color { get; set; }
        public required int Race { get; set; }
        public required DateOnly BirthDate { get; set; }
        public string? Microchip { get; set; }
    }
}
