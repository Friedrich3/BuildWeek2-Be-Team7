namespace BuildWeek2_Be_Team7.Settings
{
    public class Jwt
    {
        public required string SecurityKey { get; set; }
		 public required string Issuer { get; set; }
		 public required string Audience { get; set; }
 		 public required int ExpiresInMinutes { get; set; }
    }
}
