using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PizzaStore.Data;
using PizzaStore.Models;
using PizzaStore.Models.ModelBusinessLayer;

namespace PizzaStore.Controllers
{
    public class CustomerController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            PizzaContext context = new PizzaContext();

            if (context.Customer.Any(e => e.Email == Email && e.Password == Password)){
                Customer customer = (Customer)context.Customer.Single(e => e.Email == Email && e.Password == Password);
                TempData["Customer"] = customer.FirstName + " " + customer.LastName;


                Order order = new Order();
                order.Customer = customer;
                order.CustomerId = customer.Id;
                OrderBusinessLayer orderBusinessLayer = new OrderBusinessLayer();
                orderBusinessLayer.AddDetail(order.CustomerId);

                TempData["CustomerObj"] = customer;

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
                PizzaContext context = new PizzaContext();
                if (context.Customer.Any(e => e.Email == customer.Email))
                {
                    ViewBag.DupEmail = "True";
                    return View();
                }
                CustomerBusinessLayer customerBusinessLayer = new CustomerBusinessLayer();
                customerBusinessLayer.AddCustomer(customer);
                return RedirectToAction("Login", "Customer");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            TempData["Customer"] = "";
            return RedirectToAction("SignUp", "Customer");
        }
    }
}
