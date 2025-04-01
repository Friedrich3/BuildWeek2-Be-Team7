namespace BuildWeek2_Be_Team7.DTOs.MedicalExam
{
    public class PetInfoShowExam
    {
        public required Guid ExamId { get; set; }
        public required DateTime ExamDate { get; set; }
        public string Treatment { get; set; }
        public string Diagnosis { get; set; }
        public required string State { get; set; }
        public required string VetName { get; set; }
    }
}
