using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BuildWeek2_Be_Team7.Models.Auth;

namespace BuildWeek2_Be_Team7.Models.Pharmacy
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public required string IdPharmacist { get; set; }
        [ForeignKey(nameof(IdPharmacist))]
        public required ApplicationUser Pharmacist { get; set; }
        [Required, StringLength(17)]
        public required string CfClient { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public Guid? IdPrescription { get; set; }
        [ForeignKey(nameof(IdPrescription))]
        public Prescription? Prescription { get; set; }
        [Required]
        public decimal Total { get; set; }
        public ICollection<OrderProd>? OrderProds { get; set; }

    }
}
