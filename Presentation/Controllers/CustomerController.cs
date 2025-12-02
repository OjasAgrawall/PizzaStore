using Humanizer;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Domain.Entities;
using PizzaStore.Infrastructure.Data;
using PizzaStore.Infrastructure.ModelBusinessLayer;

namespace PizzaStore.Presentation.Controllers
{

    public class CustomerController : Controller
    {
        private readonly PizzaContext context;

        public CustomerController(PizzaContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            if (context.Customer.Any(e => e.Email == Email && e.Password == Password)){
                Customer customer = (Customer)context.Customer.Single(e => e.Email == Email && e.Password == Password);
                customer.FirstName = customer.FirstName.Titleize();
                customer.LastName = customer.LastName.Titleize();
                TempData["Customer"] = customer.FirstName + " " + customer.LastName;
                TempData["CustomerId"] = customer.Id.ToString();

                return RedirectToAction("Index", "Home");
            }
            ViewBag.Exists = "False";
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (context.Customer.Any(e => e.Email == customer.Email))
                {
                    ViewBag.DupEmail = "True";
                    return View();
                }
                CustomerRepository customerBusinessLayer = new CustomerRepository(context);
                customerBusinessLayer.AddCustomer(customer);

                Order order = new Order();
                order.Customer = context.Customer.Single(c => c.Email == customer.Email);

                OrderRepository orderBusinessLayer = new OrderRepository(context);
                orderBusinessLayer.AddCustomerId(order.Customer.Id);

                return RedirectToAction("Login", "Customer");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            TempData["Customer"] = null;
            TempData["CustomerId"] = null;
            return RedirectToAction("SignUp", "Customer");
        }
    }
}
