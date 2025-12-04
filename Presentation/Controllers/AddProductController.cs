using Microsoft.AspNetCore.Mvc;
using PizzaStore.Application.Interfaces;
using PizzaStore.Domain.Entities;

namespace PizzaStore.Presentation.Controllers
{
    public class AddProductController(IProductService productService) : Controller
    {
        public IActionResult Index()
        {

            List<Product> allProducts = productService.GetAllProducts().ToList();

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
                productService.AddProduct(products);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product products = productService.GetById(id);
            return View(products);
        }

        [HttpPost]
        public IActionResult Edit(Product products)
        {
            if (ModelState.IsValid)
            {
                productService.UpdateProduct(products);
                return RedirectToAction("Index");
            }
            return View(products);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }

    }
}
