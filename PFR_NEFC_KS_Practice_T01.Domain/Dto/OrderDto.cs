using System.Collections.ObjectModel;

namespace PFR_NEFC_KS_Practice_T01.Domain.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }

        public ICollection<OrderItemDto>? OrderItems { get; set; } = new Collection<OrderItemDto>();
    }
}
