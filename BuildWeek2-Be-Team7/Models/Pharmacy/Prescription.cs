using System.ComponentModel.DataAnnotations;

namespace BuildWeek2_Be_Team7.Models.Pharmacy
{
    public class Prescription
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string DoctorCode { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }

    }
}
