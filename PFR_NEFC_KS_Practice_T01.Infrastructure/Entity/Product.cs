using System.ComponentModel.DataAnnotations;

namespace PFR_NEFC_KS_Practice_T01.Infrastructure.Entity
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
