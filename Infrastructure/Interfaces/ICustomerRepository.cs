using PizzaStore.Domain.Entities;

namespace PizzaStore.Infrastructure.Interfaces
{
    public interface ICustomerRepository
    {
        public void AddCustomer(Customer customer);

        public void AddAddress(int id, string address);
    }
}
