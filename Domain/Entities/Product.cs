using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaStore.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }

        public string? Descriptions { get; set; }

        public string? Image { get; set; }
    }
}
