using PizzaStore.Domain.Entities;
using PizzaStore.Infrastructure.Interfaces;

namespace PizzaStore.Application.Interfaces
{
    public interface ICustomerService : ICustomerRepository
    {
        public Customer GetById(int id);

        public Customer GetByEmail(string email);
        public Customer UserMatch(string Email, string Password);
    }
}
