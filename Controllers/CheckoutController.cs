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
        private readonly PizzaContext context;

        public CheckoutController(PizzaContext _context)
        {
            context = _context;
        }
        public Customer GetCustomerFromTD()
        {
            Customer customer = new Customer();


            int customerId = int.Parse(TempData.Peek("CustomerId").ToString());
            customer = context.Customer.Single(c => c.Id == customerId);

            return customer;
        }
        public IActionResult Index()
        {
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
                return View(customer);
            }

            return RedirectToAction("Confirm", new { method = "delivery" });
        }

        [HttpPost]
        public IActionResult Delivery(int Id, string Address)
        {
            if (Address != null)
            {
                CustomerBusinessLayer customerBusinessLayer = new CustomerBusinessLayer();
                customerBusinessLayer.AddAddress(Id, Address);
                return RedirectToAction("Confirm", new { method = "delivery" });
            }

            ViewBag.IsAddress = "false";
            return View();
        }
        public IActionResult Confirm(string method)
        {
            Customer customer = GetCustomerFromTD();
            ViewBag.Method = method;
            ViewBag.Address = customer.Address;


            Order order = context.Orders.Single(o => o.CustomerId == customer.Id);
            DateTime dateTime = DateTime.Now;
            
            OrderBusinessLayer orderBusinessLayer = new OrderBusinessLayer();
            orderBusinessLayer.AddOrderPlaced(order.Id, dateTime);

            List<OrderDetail> orderDetails = context.OrderDetails
                .Where(oD => oD.OrderId == order.Id)
                .ToList();

            foreach (OrderDetail orderDetail in orderDetails)
            {
                orderDetail.Product = context.Products.Single(p => p.Id == orderDetail.ProductId);
            }

            return View(orderDetails);
        }
    }
}
