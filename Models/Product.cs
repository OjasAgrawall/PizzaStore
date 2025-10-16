using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        public String Name { get; set; } = null!;

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }

        public String? Descriptions { get; set; }
    }
}
