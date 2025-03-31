using System.ComponentModel.DataAnnotations;

namespace BuildWeek2_Be_Team7.Models.Pharmacy
{
    public class Prescription
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public required string DoctorCode { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public Order? Order { get; set; }

    }
}
