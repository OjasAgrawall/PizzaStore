using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Entities;
using PizzaStore.Infrastructure.Data;
using PizzaStore.Infrastructure.Interfaces;
using System.Data;

namespace PizzaStore.Infrastructure.ModelBusinessLayer
{
    public class OrderDetailsRepository(PizzaContext context) : IOrderDetailsRepository
    {
        public void AddItem(Product product, int quantity, int orderId)
        {
            context.Database.ExecuteSqlInterpolated($"EXECUTE spAddItem {quantity}, {product.Id}, {orderId}");
        }
        public void DeleteItem(int id)
        {
            context.Database.ExecuteSqlInterpolated($"EXECUTE spDeleteItem {id}");
        }
        public void UpdateItem(int id, int quantity)
        {
            context.Database.ExecuteSqlInterpolated($"EXECUTE spUpdateItem {id}, {quantity}");
        }

        public IEnumerable<OrderDetail> GetAllItems()
        {
            return context.OrderDetails.ToList();
        }
    }
}
