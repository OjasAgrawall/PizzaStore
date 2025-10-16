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
        public IActionResult Add(int id)
        {
            PizzaContext context = new PizzaContext();
            Product product = context.Products.Single(p =>  p.Id == id);
            OrderDetail detail = new OrderDetail();
            detail.Product = product;
            detail.ProductId = product.Id;
            return View(detail);
        }

        [HttpPost]
        public IActionResult Add(int Id, int quantity)
        {
            PizzaContext context = new PizzaContext();

            Product product = context.Products.Single(p => p.Id == Id);

            OrderDetailsBusinessLayer orderDetailsBusinessLayer = new OrderDetailsBusinessLayer();
            orderDetailsBusinessLayer.AddItem(product, quantity);

            return RedirectToAction("Index");
        }

        public IActionResult ViewCart()
        {
            PizzaContext context = new PizzaContext();
            List<OrderDetail> orders = context.OrderDetails.ToList();

            foreach (OrderDetail orderDetail in orders)
            {
                orderDetail.Product = context.Products.Single(p => p.Id ==  orderDetail.ProductId);
            }

            return View(orders);
        }
    }

}
