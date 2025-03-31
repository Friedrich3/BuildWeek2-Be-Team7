using BuildWeek2_Be_Team7.Models.Animali;
using BuildWeek2_Be_Team7.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BuildWeek2_Be_Team7.DTOs.Client;

namespace BuildWeek2_Be_Team7.DTOs.Pet
{
    public class PetDto
    {
        public Guid PetId { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public required string Name { get; set; }
        public required string Color { get; set; }
        public required string Race { get; set; }
        public required DateOnly BirthDate { get; set; }
        public string Microchip { get; set; }
        public Owner Owner { get; set; }
    }
}
