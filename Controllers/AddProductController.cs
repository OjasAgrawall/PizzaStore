using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Data;
using PizzaStore.Models;

namespace PizzaStore.Controllers
{
    public class AddProductController : Controller
    {
        public IActionResult Index()
        {
            PizzaContext context = new PizzaContext();

            List<Products> allProducts = context.Products.ToList();

            return View(allProducts);
        }

        [HttpGet]
        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Create(Products products)
        {
            if (ModelState.IsValid)
            {
                PizzaContext context = new PizzaContext();

                context.Products.Add(products);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
