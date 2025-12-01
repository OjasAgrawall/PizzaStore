using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Entities;

namespace PizzaStore.Infrastructure.Data
{
    public class PizzaContext : DbContext
    {

        public DbSet<Customer> Customer { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;



        public PizzaContext(DbContextOptions options) : base(options) {}
        
    }
} 