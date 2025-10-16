using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Data;
using PizzaStore.Models;

namespace PizzaStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            PizzaContext context = new PizzaContext();

            var products = context.Products
                .OrderBy(p => p.Price);

            ViewBag.Products = products;


            return View();
        }
    }
}
