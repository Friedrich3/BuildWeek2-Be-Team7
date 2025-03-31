using System.ComponentModel.DataAnnotations;

namespace BuildWeek2_Be_Team7.DTOs.Account
{
    public class RegisterRequestDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string CodiceFiscale { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required DateOnly BirthDate { get; set; }
        
    }
}
