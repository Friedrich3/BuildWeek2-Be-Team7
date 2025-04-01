namespace BuildWeek2_Be_Team7.DTOs.Client
{
    public class ClientDto
    {
        public Guid IdOwner { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string CodiceFiscale { get; set; }
        public string Email { get; set; }
    }
}
