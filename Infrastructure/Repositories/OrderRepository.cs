using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Entities;
using PizzaStore.Infrastructure.Data;
using PizzaStore.Infrastructure.Interfaces;
using System.Data;

namespace PizzaStore.Infrastructure.ModelBusinessLayer
{
    public class OrderRepository(PizzaContext context) : IOrderRepository
    {
    
        public void AddCustomerId(int CustomerId)
        {
            context.Database.ExecuteSqlInterpolated($"EXECUTE spAddDetailsToOrder {CustomerId}");
        }
        public void AddOrderPlaced(int Id, DateTime orderPlaced)
        {
            context.Database.ExecuteSqlInterpolated($"EXECUTE spAddOrderPlaced {Id}, {orderPlaced}");
        }

        public Order GetById(int id)
        {
           return context.Orders.Where(o => o.Id == id).First();
        }

        public Order GetByCustomerId(int id)
        {
            return context.Orders.Where(o => o.CustomerId == id).First();
        }
    }
}
