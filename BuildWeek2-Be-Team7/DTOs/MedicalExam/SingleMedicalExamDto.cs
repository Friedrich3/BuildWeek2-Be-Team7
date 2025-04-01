using BuildWeek2_Be_Team7.Models.Auth;

namespace BuildWeek2_Be_Team7.DTOs.MedicalExam
{
    public class SingleMedicalExamDto
    {
        public required Guid ExamId { get; set; }
        public required DateTime ExamDate { get; set; }
        public required string State { get; set; }
        public required string VetName { get; set; }
        public required string PetName { get; set; }
        public string? OwnerName { get; set; } = null;

    }
}
