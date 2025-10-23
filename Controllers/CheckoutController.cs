using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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


            int customerId = int.Parse(TempData.Peek("CustomerId").ToString());
            customer = context.Customer.Single(c => c.Id == customerId);

            return customer;
        }
        public IActionResult Index()
        {
            PizzaContext context = new PizzaContext();
            int customerId = int.Parse(TempData.Peek("CustomerId").ToString());
            int OrderId = context.Orders.Single(o => o.CustomerId == customerId).Id;

            List<OrderDetail> orderDetails = context.OrderDetails
                .Where(orderD => orderD.OrderId == OrderId)
                .ToList();

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

        [HttpGet]
        public IActionResult Delivery()
        {
            Customer customer = GetCustomerFromTD();
            
            if (customer.Address == null)
            {
                ViewBag.IsAddress = "false";
            }

            return View(customer);
        }

        [HttpPost]
        public IActionResult Delivery(int Id, string Address)
        {
            if (Address != null)
            {
                CustomerBusinessLayer customerBusinessLayer = new CustomerBusinessLayer();
                customerBusinessLayer.AddAddress(Id, Address);
                Debug.WriteLine(Id);
                return RedirectToAction("Confirm", "method=Delivery");
            }
            ViewBag.IsAddress = "false";
            return View();
        }

        [HttpGet]
        public IActionResult Confirm(string method)
        {
            Customer customer = GetCustomerFromTD();
            ViewBag["method"] = method;
            return View(customer);
        }

        [HttpPost]
        public IActionResult Confirm()
        {
            PizzaContext context = new PizzaContext();
            Order order = new Order();

            Customer customer = GetCustomerFromTD();
            
            foreach(OrderDetail orderDetail in context.OrderDetails)
            {
                order.OrderDetails.Add(orderDetail);
                context.OrderDetails.Remove(orderDetail);
            }






            return View();
        }
    }
}
