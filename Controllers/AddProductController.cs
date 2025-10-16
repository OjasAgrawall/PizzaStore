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

            List<Product> allProducts = context.Products.ToList();

            return View(allProducts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product products)
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
            Product products = context.Products.Single(pizza => pizza.Id == id);
            return View(products);
        }

        [HttpPost]
        public IActionResult Edit(Product products)
        {
            Debug.WriteLine(products.Id);


            if (ModelState.IsValid)
            {
                Debug.WriteLine(products.Id);

                ProductBusinessLayer productBusinessLayer = new ProductBusinessLayer();
                productBusinessLayer.UpdateProduct(products);
                return RedirectToAction("Index");
            }
            return View(products);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            ProductBusinessLayer productBusinessLayer = new ProductBusinessLayer();
            productBusinessLayer.DeleteProduct(id);

            return RedirectToAction("Index");
        }

    }
}
