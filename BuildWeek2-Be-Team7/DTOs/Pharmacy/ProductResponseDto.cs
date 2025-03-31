using System.ComponentModel.DataAnnotations;

namespace BuildWeek2_Be_Team7.DTOs.Pharmacy
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required double Price { get; set; }

        public required bool isMed { get; set; }

        public DrawerDto Drawer { get; set; }

        public CompanyDto Company { get; set; }

        public required string CategoryName { get; set; }
    }
}
