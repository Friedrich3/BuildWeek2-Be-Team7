using System.ComponentModel.DataAnnotations;

namespace BuildWeek2_Be_Team7.Models.Animali
{
    public class Race
    {
        [Key]
        public int RaceId { get; set; }
        [Required]
        public required string Name { get; set; }

        public ICollection<Pet> Pets { get; set; }
    }
}
