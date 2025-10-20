using Microsoft.AspNetCore.Mvc;

namespace PizzaStore.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            if (TempData.Peek("Customer") == "")
            {
                return RedirectToAction("Login", "Customer", new {Checkout = "true"});
            }
            return View();
        }
    }
}
