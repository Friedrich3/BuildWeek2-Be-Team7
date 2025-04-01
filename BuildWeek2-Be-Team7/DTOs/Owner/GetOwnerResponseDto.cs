namespace BuildWeek2_Be_Team7.DTOs.Owner
{
    public class GetOwnerResponseDto
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required DateOnly Birthdate { get; set; }
        public required string CodiceFiscale { get; set; }
        public required string Email { get; set; }
        public List<SinglePetDto> Pets { get; set; }
        public List<SingleOrderDto> Orders { get; set; }
    }
}
