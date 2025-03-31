using System.ComponentModel.DataAnnotations;

namespace BuildWeek2_Be_Team7.DTOs.Pharmacy
{
    public class CompanyDto
    {
        public required string Name { get; set; }

        public required string Address { get; set; }

        public required string Tel { get; set; }
    }
}
