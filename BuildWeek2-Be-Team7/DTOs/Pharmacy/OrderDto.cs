using BuildWeek2_Be_Team7.Models.Auth;
using BuildWeek2_Be_Team7.Models.Pharmacy;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BuildWeek2_Be_Team7.DTOs.Client;

namespace BuildWeek2_Be_Team7.DTOs.Pharmacy
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public required string Pharmacist { get; set; }
        public ClientDto Client { get; set; }
        public DateTime Date { get; set; }
        public PrescriptionDto? Prescription { get; set; }
        public decimal Total { get; set; }
        public List<ProductOrderDto>? Products { get; set; }
    }
}
