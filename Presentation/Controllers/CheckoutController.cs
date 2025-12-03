using Microsoft.AspNetCore.Mvc;
using PizzaStore.Application.Interfaces;
using PizzaStore.Application.Services;
using PizzaStore.Domain.Entities;
using PizzaStore.Infrastructure.Data;
using PizzaStore.Infrastructure.ModelBusinessLayer;

namespace PizzaStore.Presentation.Controllers
{
    public class CheckoutController(
        IOrderService orderService,
        IOrderDetailsService orderDetailsService,
        ICustomerService customerService,
        IProductService productService) : Controller
    {
        public Customer GetCustomerFromTD()
        {
            Customer customer = new Customer();

            int customerId = int.Parse(TempData.Peek("CustomerId").ToString());
            customer = customerService.GetById(customerId);

            return customer;
        }
        public IActionResult Index()
        {
            int customerId = GetCustomerFromTD().Id;
            int OrderId = orderService.GetByCustomerId(customerId).Id;

            List<OrderDetail> orderDetails = orderDetailsService.GetByOrderId(OrderId).ToList(); 

            decimal totalPrice = orderDetailsService.TotalPrice(OrderId);

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
        public IActionResult Delivery(int id, string address)
        {
            if (address != null)
            {
                customerService.AddAddress(id, address);
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

            Order order = orderService.GetByCustomerId(customer.Id);
            orderService.AddOrderPlaced(order.Id, DateTime.Now);
            
            List<OrderDetail> orderDetails = orderDetailsService.GetByOrderId(order.Id).ToList();

            foreach (OrderDetail orderDetail in orderDetails)
            {
                orderDetail.Product = productService.GetById(orderDetail.ProductId);
            }

            return View(orderDetails);
        }
    }
}
