namespace BuildWeek2_Be_Team7.DTOs.Account
{
    public class TokenResponse
    {
        public required string Token { get; set; }
        public required DateTime Expires { get; set; }
    }
}
