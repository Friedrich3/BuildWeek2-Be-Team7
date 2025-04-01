using BuildWeek2_Be_Team7.Models.Animali;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildWeek2_Be_Team7.DTOs.Owner
{
    public class SinglePetDto
    {
        public DateOnly RegistrationDate { get; set; }
        public required string Name { get; set; }
        public required string Color { get; set; }
        public required string Race { get; set; }
        public required DateOnly BirthDate { get; set; }
        public string? Microchip { get; set; } = null;
    }
}
