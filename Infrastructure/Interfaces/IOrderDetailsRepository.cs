using PizzaStore.Domain.Entities;

namespace PizzaStore.Infrastructure.Interfaces
{
    public interface IOrderDetailsRepository
    {
        public void AddItem(Product product, int quantity, int orderId);

        public void DeleteItem(int id);

        public void UpdateItem(int id, int quantity);

        public IEnumerable<OrderDetail> GetAllItems();

    }
}
