using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningEntityFramework.Models
{
    public class Products
    {
        public int Id { get; set; }

        public String Name { get; set; } = null!;

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
    }
}
