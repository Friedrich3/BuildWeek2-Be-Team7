using BuildWeek2_Be_Team7.DTOs.Client;

namespace BuildWeek2_Be_Team7.DTOs.Pharmacy
{
    public class AddOrder
    {
        public string? NameClient { get; set; } = null;
        public string? SurnameClient { get; set; } = null;
        public required string CodiceFiscaleClient { get; set; }
        public string? EmailClient { get; set; } = null;
        public DateOnly DateBirthClient { get; set; }
        public string? DoctorCf { get; set; } = null;
        public string? DescriptionPrescription { get; set; } = null;
        public DateTime? DatePrescription { get; set; } = null;
        public decimal Total { get; set; }
        public List<AddProductOrder>? Products { get; set; }
    }
}
