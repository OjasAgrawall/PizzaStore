using Microsoft.AspNetCore.Mvc;
using PizzaStore.Data;
using PizzaStore.Models;
using PizzaStore.Models.ModelBusinessLayer;
using System.Diagnostics;
using static NuGet.Packaging.PackagingConstants;

namespace PizzaStore.Controllers
{
    public class CheckoutController : Controller
    {

        public Customer GetCustomerFromTD()
        {
            PizzaContext context = new PizzaContext();
            Customer customer = new Customer();


            string[] customerFName = TempData.Peek("Customer").ToString().Split(" ");
            customer = context.Customer.Single(c => c.FirstName == customerFName[0]);

            return customer;
        }
        public IActionResult Index()
        {
            if (TempData.Peek("Customer") == "")
            {
                return RedirectToAction("Login", "Customer", new {Checkout = "true"});
            }

            PizzaContext context = new PizzaContext();
            List<OrderDetail> orderDetails = context.OrderDetails.ToList();

            decimal totalPrice = 0;
            foreach (OrderDetail orderDetail in orderDetails)
            {
                orderDetail.Product = context.Products.Single(p => p.Id == orderDetail.ProductId);
                totalPrice += orderDetail.Product.Price * orderDetail.Quantity;
            }
            ViewData["Total"] = totalPrice;

            return View(orderDetails);
        }

        public IActionResult Pickup()
        {
            return View();
        }

        public IActionResult Delivery()
        {
            Customer customer = GetCustomerFromTD();
            
            if (customer.Address == null)
            {
                ViewBag.IsAddress = "false";
            }

            return View();
        }
    }
}
