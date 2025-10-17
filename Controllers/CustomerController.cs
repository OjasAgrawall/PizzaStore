using Microsoft.AspNetCore.Mvc;

namespace PizzaStore.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
