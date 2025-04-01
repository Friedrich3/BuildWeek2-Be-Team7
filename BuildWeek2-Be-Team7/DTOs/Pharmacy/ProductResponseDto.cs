using System.ComponentModel.DataAnnotations;

namespace BuildWeek2_Be_Team7.DTOs.Pharmacy
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required double Price { get; set; }

        public required string Image { get; set; }

        public required bool isMed { get; set; }

        public DrawerDto? Drawer { get; set; } = null;

        public CompanyDto? Company { get; set; } = null;

        public required string CategoryName { get; set; }
    }
}
