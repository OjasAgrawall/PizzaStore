using Humanizer;
using PizzaStore.Application.Interfaces;
using PizzaStore.Domain.Entities;
using PizzaStore.Infrastructure.Interfaces;


namespace PizzaStore.Application.Services
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        public void AddAddress(int id, string address)
        {
            customerRepository.AddAddress(id, address);
        }

        public void AddCustomer(Customer customer)
        {
            customerRepository.AddCustomer(customer);
        }

        public IEnumerable<Customer> GetAll()
        {
            return customerRepository.GetAll();
        }

        public Customer GetById(int id)
        {
            return customerRepository.GetAll().Single(customer => customer.Id == id);
        }

        public Customer GetByEmail(string email)
        {
            return customerRepository.GetAll().Single(c => c.Email == email);
        }

        public Customer UserMatch(string Email, string Password)
        {
            List<Customer> Customers = GetAll().ToList();
            if (Customers.Any(c => c.Email == Email && c.Password == Password))
            {
                Customer customer = GetAll().Single(e => e.Email == Email && e.Password == Password);
                customer.FirstName = customer.FirstName.Titleize();
                customer.LastName = customer.LastName.Titleize();
                return customer;
            }
            return null;
        }
    }
}
