using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Data;
using PizzaStore.Models;
using System.Diagnostics;

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
                ProductBusinessLayer productBusinessLayer = new ProductBusinessLayer();
                productBusinessLayer.AddProduct(products);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            PizzaContext context = new PizzaContext();
            Products product = context.Products.Single(pizza => pizza.Id == id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Products products)
        {
            if (ModelState.IsValid)
            {
                ProductBusinessLayer productBusinessLayer = new ProductBusinessLayer();
                productBusinessLayer.AddProduct(products);
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
