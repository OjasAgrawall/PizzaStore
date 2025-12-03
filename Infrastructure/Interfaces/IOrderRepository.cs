using PizzaStore.Domain.Entities;

namespace PizzaStore.Infrastructure.Interfaces
{
    public interface IOrderRepository
    {
        public void AddCustomerId(int CustomerId);

        public void AddOrderPlaced(int id, DateTime orderPlaced);

        public Order GetById(int id);

        public Order GetByCustomerId(int id);

    }
}
