using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Entities;
using PizzaStore.Infrastructure.Data;
using PizzaStore.Infrastructure.Interfaces;
using System.Data;

namespace PizzaStore.Infrastructure.ModelBusinessLayer
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PizzaContext context;

        public OrderRepository(PizzaContext _context)
        {
            context = _context;
        }
        public void AddCustomerId(int CustomerId)
        {
            context.Database.ExecuteSqlInterpolated($"EXECUTE spAddDetailsToOrder {CustomerId}");
        }
        public void AddOrderPlaced(int Id, DateTime orderPlaced)
        {
            context.Database.ExecuteSqlInterpolated($"EXECUTE spAddDetailsToOrder {Id}, {orderPlaced}");
        }
    }
}
