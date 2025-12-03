using Humanizer;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Application.Interfaces;
using PizzaStore.Domain.Entities;
using PizzaStore.Infrastructure.Data;
using PizzaStore.Infrastructure.ModelBusinessLayer;

namespace PizzaStore.Presentation.Controllers
{

    public class CustomerController( 
        ICustomerService customerService,
        IOrderService orderService
        ) : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            if (customerService.UserMatch(Email, Password) != null)
            {
                Customer customer = customerService.UserMatch(Email, Password);
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
                if (customerService.GetByEmail(customer.Email) != null)
                {
                    ViewBag.DupEmail = "True";
                    return View();
                }
                customerService.AddCustomer(customer);

                int customerId = customerService.GetByEmail(customer.Email).Id;

                orderService.AddCustomerId(customerId);


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
