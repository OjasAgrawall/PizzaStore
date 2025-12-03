using Microsoft.AspNetCore.Mvc;
using PizzaStore.Domain.Entities;
using PizzaStore.Infrastructure.Interfaces;

namespace PizzaStore.Application.Interfaces
{
    public interface IOrderDetailsService : IOrderDetailsRepository
    {
        public void CombineDuplicates();

        public IEnumerable<OrderDetail> GetByOrderId(int orderId);

        public OrderDetail GetById(int id);

        public decimal TotalPrice(int OrderId);

        public OrderDetail IsQuantityPositive(int productId, int quantity);

    }
}
