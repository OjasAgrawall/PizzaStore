using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        [Range(0, 10)]
        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public int? OrderId { get; set; }

        public Order? Order { get; set; }

        public Product Product { get; set; } = null!;
    }
}