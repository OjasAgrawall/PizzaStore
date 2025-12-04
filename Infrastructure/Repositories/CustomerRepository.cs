using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Entities;
using PizzaStore.Infrastructure.Data;
using PizzaStore.Infrastructure.Interfaces;

namespace PizzaStore.Infrastructure.ModelBusinessLayer
{
    public class CustomerRepository(PizzaContext context) : ICustomerRepository
    {
        public void AddCustomer(Customer customer)
        {
            context.Database.ExecuteSqlInterpolated($"EXECUTE spAddCustomer {customer.FirstName}, {customer.LastName}, {customer.Email}, {customer.Password}, {customer.Address}, {customer.Phone}");
        }

        public void AddAddress(int id, string address)
        {
            context.Database.ExecuteSqlInterpolated($"EXECUTE spAddCustomerAddress {id}, {address}");
        }

        public IEnumerable<Customer> GetAll()
        {
            return context.Customer.ToList();
        }
    }
}
