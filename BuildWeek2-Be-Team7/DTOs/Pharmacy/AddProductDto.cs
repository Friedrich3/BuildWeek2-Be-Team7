namespace BuildWeek2_Be_Team7.DTOs.Pharmacy
{
    public class AddProductDto
    {
        public required string Name { get; set; }
        public required string Image { get; set; }
        public required double Price { get; set; }
        public required bool isMed { get; set; }
        public int DrawerId { get; set; }
        public int CategoryId { get; set; }
        public required string CompanyName { get; set; }
        public required string Address { get; set; }
        public required string Tel { get; set; }
    }
}
