using System.ComponentModel.DataAnnotations;

namespace PFR_NEFC_KS_Practice_T01.Infrastructure.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        [MaxLength(100)]
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
