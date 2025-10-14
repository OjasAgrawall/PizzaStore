using LearningEntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LearningEntityFramework.Data
{
    public class PizzaContext : DbContext
    {

        public DbSet<Customer> Customer { get; set; } = null!;

        public DbSet<Products> Products { get; set; } = null!;

        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=Pizza;Trusted_Connection=True;TrustServerCertificate=True;");
        }

    }
}