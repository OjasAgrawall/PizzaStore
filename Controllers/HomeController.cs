using System.Diagnostics;
using LearningEntityFramework.Data;
using LearningEntityFramework.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningEntityFramework.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            PizzaContext context = new PizzaContext();

            var veggieSpecial = context.Products
                .Where(p => p.Name == "Veggie Special Pizza")
                .FirstOrDefault();

            if (veggieSpecial is Products)
            {
                veggieSpecial.Price = 10.99M;
            }

            context.SaveChanges();

            var products = context.Products
                .Where(p => p.Price >= 10)
                .OrderBy(p => p.Id);

            ViewBag.Products = products;


            return View();
        }
    }
}
