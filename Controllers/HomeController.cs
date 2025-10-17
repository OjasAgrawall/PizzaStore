using Microsoft.AspNetCore.Mvc;
using PizzaStore.Data;
using PizzaStore.Models;
using PizzaStore.Models.ModelBusinessLayer;
using System.Diagnostics;
using static NuGet.Packaging.PackagingConstants;

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
            Product product = context.Products.Single(p => p.Id == id);
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
            List<OrderDetail> orders = context.OrderDetails.ToList();

            OrderDetailsBusinessLayer orderDetailsBusinessLayer = new OrderDetailsBusinessLayer();
            orderDetailsBusinessLayer.AddItem(product, quantity);

            orders = context.OrderDetails
                .OrderBy(order => order.ProductId)
                .ToList();


            for (int i = 0; i < orders.ToArray().Length - 1; i++)
            {
                if (orders[i].ProductId == orders[i + 1].ProductId)
                {
                    int totalQuantity = orders[i + 1].Quantity + orders[i].Quantity;
                    orderDetailsBusinessLayer.UpdateItem(orders[i + 1].Id, totalQuantity);
                    orderDetailsBusinessLayer.DeleteItem(orders[i].Id);
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult ViewCart()
        {
            PizzaContext context = new PizzaContext();
            List<OrderDetail> orders = context.OrderDetails.ToList();

            foreach (OrderDetail orderDetail in orders)
            {
                orderDetail.Product = context.Products.Single(p => p.Id == orderDetail.ProductId);

            }
            return View(orders);

        }
    }
}