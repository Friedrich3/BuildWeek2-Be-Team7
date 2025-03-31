using System.ComponentModel.DataAnnotations.Schema;

namespace BuildWeek2_Be_Team7.Models.Pharmacy
{
    public class OrderProd
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("OrderId")]
        public required Order Order { get; set; }

        [ForeignKey("ProductId")]
        public required Product Product { get; set; }
    }
}
