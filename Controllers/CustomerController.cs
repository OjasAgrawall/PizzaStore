using Microsoft.AspNetCore.Mvc;
using PizzaStore.Data;
using PizzaStore.Models;
using PizzaStore.Models.ModelBusinessLayer;

namespace PizzaStore.Controllers
{
    public class CustomerController : Controller
    {

        public IActionResult Login()
        {
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
            CustomerBusinessLayer customerBusinessLayer = new CustomerBusinessLayer();
            customerBusinessLayer.AddCustomer(customer);
            return RedirectToAction("Index", "Home");
        }
    }
}
