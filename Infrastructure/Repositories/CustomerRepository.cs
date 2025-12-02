using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Entities;
using PizzaStore.Infrastructure.Data;
using PizzaStore.Infrastructure.Interfaces;
using System.Data;

namespace PizzaStore.Infrastructure.ModelBusinessLayer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly PizzaContext context;

        public CustomerRepository(PizzaContext _context)
        {
            context = _context;
        }
        public void AddCustomer(Customer customer)
        {
            context.Database.ExecuteSqlInterpolated($"EXECUTE spAddCustomer {customer.FirstName}, {customer.LastName}, {customer.Email}, {customer.Password}, {customer.Address}, {customer.Phone}");
        }

        public void AddAddress(int id, string address)
        {
            context.Database.ExecuteSqlInterpolated($"EXECUTE spAddCustomerAddress {id}, {address}");
        }
    }
}
