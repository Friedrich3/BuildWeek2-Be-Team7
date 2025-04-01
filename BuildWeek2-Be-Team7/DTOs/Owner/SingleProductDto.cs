using System.ComponentModel.DataAnnotations;

namespace BuildWeek2_Be_Team7.DTOs.Owner
{
    public class SingleProductDto
    {
        public required string Name { get; set; }
        public required string Image { get; set; }
        public required double Price { get; set; }
        public required bool isMed { get; set; }
    }
}
