using BuildWeek2_Be_Team7.Models.Pharmacy;
using System.ComponentModel.DataAnnotations;

namespace BuildWeek2_Be_Team7.DTOs.Pharmacy
{
    public class PrescriptionDto
    {
        public required string DoctorCf { get; set; }
        public required string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
