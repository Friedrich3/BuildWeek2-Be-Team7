using BuildWeek2_Be_Team7.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildWeek2_Be_Team7.DTOs.MedicalExam
{
    public class MedicalExamResponseDto
    {
        public required Guid ExamId { get; set; }
        public required DateTime ExamDate { get; set; }
        public string? Treatment { get; set; } = null;
        public string? Diagnosis { get; set; } = null; 
        public required string State { get; set; }
        public required VeterinarioDto Vet { get; set; }
        public required string LastModified { get; set; }

    }
}
