namespace BuildWeek2_Be_Team7.DTOs.Pharmacy
{
    public class ProductOrderDto
    {
        public required string Name { get; set; }

        public required double Price { get; set; }

        public required string Image { get; set; }
        public string CompanyName { get; set; }
        public int Quantity { get; set; }
    }
}
