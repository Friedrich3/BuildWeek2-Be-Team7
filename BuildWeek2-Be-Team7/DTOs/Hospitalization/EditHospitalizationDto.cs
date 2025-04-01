namespace BuildWeek2_Be_Team7.DTOs.Hospitalization
{
    public class EditHospitalizationDto
    {
        public required DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; } = null;
    }
}
