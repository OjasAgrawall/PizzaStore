namespace PizzaStore.Infrastructure.Interfaces
{
    public interface IOrderRepository
    {
        public void AddCustomerId(int CustomerId);

        public void AddOrderPlaced(int Id, DateTime orderPlaced);
    }
}
