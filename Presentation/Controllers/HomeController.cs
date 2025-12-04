using Microsoft.AspNetCore.Mvc;
using PizzaStore.Application.Interfaces;
using PizzaStore.Domain.Entities;

namespace PizzaStore.Presentation.Controllers
{
    public class HomeController(
        IProductService productService,
        IOrderService orderService,
        IOrderDetailsService orderDetailsService) : Controller
    {
        public IActionResult Index()
        {

            List<Product> products = productService.GetAllProducts().ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            if (TempData.Peek("Customer") is null)
            {
                return RedirectToAction("Login", "Customer");
            }
            Product product = productService.GetById(id);
            OrderDetail detail = new OrderDetail();
            detail.Product = product;
            detail.ProductId = product.Id;
            return View(detail);
        }

        [HttpPost]
        public IActionResult Add(int id, int quantity) //id is product id
        {
            Product product = productService.GetById(id);
            int customerId = int.Parse(TempData.Peek("CustomerId").ToString());
            Order order = orderService.GetByCustomerId(customerId);

            orderDetailsService.AddItem(product, quantity, order.Id);

            if (quantity <= 0)
            {
                ViewBag.QuantityError = "true";
                return View(orderDetailsService.IsQuantityPositive(product.Id, quantity));
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
            int OrderId = orderService.GetByCustomerId(customerId).Id;

            List<OrderDetail> orderDetails = orderDetailsService.GetByOrderId(OrderId).ToList();

            decimal totalPrice = orderDetailsService.TotalPrice(OrderId);

            ViewData["Total"] = totalPrice;
            return View(orderDetails);
        }

        [HttpGet]
        public IActionResult QuantityDown(int id)
        {
            orderDetailsService.Decrease(id);

            return RedirectToAction("ViewCart");
        }

        [HttpGet]
        public IActionResult QuantityUp(int id)
        {
            orderDetailsService.Increase(id);

            return RedirectToAction("ViewCart");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            orderDetailsService.DeleteItem(id);
            return RedirectToAction("ViewCart");
        }
    }
}