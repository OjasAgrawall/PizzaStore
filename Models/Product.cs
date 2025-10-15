using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningEntityFramework.Models
{
    public class Product
    {
        public int Id { get; set; }

        public String Name { get; set; } = null!;

        [Column(TypeName = "decimal(0,2)")]
        public decimal Price { get; set; }
    }
}
