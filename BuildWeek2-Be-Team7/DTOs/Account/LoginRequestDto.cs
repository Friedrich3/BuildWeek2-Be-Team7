namespace BuildWeek2_Be_Team7.DTOs.Account
{
    public class LoginRequestDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
