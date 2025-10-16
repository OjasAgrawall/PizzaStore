using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Data;
using PizzaStore.Models;
using PizzaStore.Models.ModelBusinessLayer;

namespace PizzaStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            PizzaContext context = new PizzaContext();

            List<Product> products = context.Products.ToList();


            return View(products);
        }

        [HttpGet]
        public IActionResult Add(int Id, int quantity)
        {
            PizzaContext context = new PizzaContext();

            Product product = context.Products.Single(p => p.Id == Id);

            OrderDetailsBusinessLayer orderDetailsBusinessLayer = new OrderDetailsBusinessLayer();
            orderDetailsBusinessLayer.AddItem(product, quantity);


            return RedirectToAction("Index");
        }

        public IActionResult test()
        {
            return View();
        }
    }
}
