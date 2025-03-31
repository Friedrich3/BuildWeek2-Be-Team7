namespace BuildWeek2_Be_Team7.DTOs.Pharmacy
{
    public class ChangeProductDto
    {
        public required string Name { get; set; }
        public string? ImageView { get; set; }
        public IFormFile? Image { get; set; } = null;
        public required double Price { get; set; }
        public bool isMed { get; set; }
        public int DrawerId { get; set; }
    }
}
