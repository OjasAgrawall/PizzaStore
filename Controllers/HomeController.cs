using Microsoft.AspNetCore.Mvc;
using PizzaStore.Data;
using PizzaStore.Models;
using PizzaStore.Models.ModelBusinessLayer;

namespace PizzaStore.Controllers
{
    public class HomeController : Controller
    {

        private readonly PizzaContext context;

        public HomeController(PizzaContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {

            List<Product> products = context.Products.ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            if (TempData.Peek("Customer") == "")
            {
                return RedirectToAction("Login", "Customer");
            }

            Product product = context.Products.Single(p => p.Id == id);
            OrderDetail detail = new OrderDetail();
            detail.Product = product;
            detail.ProductId = product.Id;
            return View(detail);
        }

        [HttpPost]
        public IActionResult Add(int Id, int quantity)
        {

            Product product = context.Products.Single(p => p.Id == Id);

            if (quantity <= 0)
            {
                OrderDetail detail = new OrderDetail();
                detail.Product = product;
                detail.ProductId = product.Id;
                ViewBag.Negative = "true";
                return View(detail);
            }

            int customerId = int.Parse(TempData.Peek("CustomerId").ToString());

            //Link orderDetail to Product and Order, and give it the quantity, productid, and orderid 
            Order order = context.Orders.Single(o => o.CustomerId == customerId);
            OrderDetail orderDetail = new OrderDetail {Quantity = quantity, ProductId = Id, OrderId = order.Id, Product = product, Order = order};

            //Add orderdetails to db
            OrderDetailsBusinessLayer orderDetailsBusinessLayer = new OrderDetailsBusinessLayer();
            orderDetailsBusinessLayer.AddItem(orderDetail.Product, orderDetail.Quantity, orderDetail.OrderId);

            List<OrderDetail> orderDetails = context.OrderDetails
                .OrderBy(order => order.ProductId)
                .ToList();

            for (int i = 0; i < orderDetails.ToArray().Length - 1; i++)
            {
                if (orderDetails[i].ProductId == orderDetails[i + 1].ProductId && orderDetails[i].OrderId == orderDetails[i + 1].OrderId)
                {
                    int totalQuantity = orderDetails[i + 1].Quantity + orderDetails[i].Quantity;
                    orderDetailsBusinessLayer.UpdateItem(orderDetails[i + 1].Id, totalQuantity);
                    orderDetailsBusinessLayer.DeleteItem(orderDetails[i].Id);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ViewCart()
        {
            if (TempData.Peek("Customer") is null)
            {
                return RedirectToAction("Login", "Customer");
            }

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

        [HttpGet]
        public IActionResult EditCart(int Id)
        {
            OrderDetail order = context.OrderDetails.Single(p => p.Id == Id);

            order.Product = context.Products.Single(p => p.Id == order.ProductId);
            return View(order);
        }

        [HttpPost]
        public IActionResult EditCart(int Id, int Quantity)
        {
            if (Quantity > 0)
            {

                OrderDetailsBusinessLayer orderDetailsBusinessLayer = new OrderDetailsBusinessLayer();
                orderDetailsBusinessLayer.UpdateItem(Id, Quantity);
                return RedirectToAction("ViewCart");
            }

            OrderDetail order = context.OrderDetails.Single(o => o.Id == Id);
            order.Product = context.Products.Single(p => p.Id == order.ProductId);
            order.Quantity = Quantity;
            return View(order);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            OrderDetailsBusinessLayer orderDetailsBusinessLayer = new OrderDetailsBusinessLayer();
            orderDetailsBusinessLayer.DeleteItem(Id);
            return RedirectToAction("ViewCart");
        }
    }
}