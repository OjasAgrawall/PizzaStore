using Microsoft.AspNetCore.Mvc;
using PizzaStore.Application.Interfaces;
using PizzaStore.Domain.Entities;
using PizzaStore.Infrastructure.Interfaces;

namespace PizzaStore.Application.Services
{
    public class OrderDetailsService(IOrderDetailsRepository orderDetailsRepo, IProductService productService) : IOrderDetailsService
    {
        public void AddItem(Product product, int quantity, int orderId)
        {
            OrderDetail? orderDetail = IsQuantityPositive(product.Id, quantity);
            if (orderDetail == null)
            {
                orderDetailsRepo.AddItem(product, quantity, orderId);
                CombineDuplicates();
            }
        }

        public void DeleteItem(int id)
        {
            orderDetailsRepo.DeleteItem(id);           
        }

        public void UpdateItem(int id, int quantity)
        {
            orderDetailsRepo.UpdateItem(id, quantity);
        }

        public IEnumerable<OrderDetail> GetAllItems()
        {
            return orderDetailsRepo.GetAllItems();
        }

        public IEnumerable<OrderDetail> GetByOrderId(int orderId)
        {
            return GetAllItems().Where(o => o.OrderId == orderId).ToList();
        }

        public OrderDetail GetById(int id)
        {
            return orderDetailsRepo.GetAllItems().Where(o => o.Id == id).Single();
        }

        public void CombineDuplicates()
        {
            List<OrderDetail> orderDetails = orderDetailsRepo.GetAllItems()
                .OrderBy(order => order.ProductId)
                .ToList();

            for (int i = 0; i < orderDetails.ToArray().Length - 1; i++)
            {
                if (orderDetails[i].ProductId == orderDetails[i + 1].ProductId && orderDetails[i].OrderId == orderDetails[i + 1].OrderId)
                {
                    int totalQuantity = orderDetails[i + 1].Quantity + orderDetails[i].Quantity;
                    orderDetailsRepo.UpdateItem(orderDetails[i + 1].Id, totalQuantity);
                    orderDetailsRepo.DeleteItem(orderDetails[i].Id);
                }
            }
        }

        public decimal TotalPrice(int OrderId)
        {
            List<OrderDetail> orderDetails = GetByOrderId(OrderId).ToList();

            decimal totalPrice = 0;
            foreach (OrderDetail orderDetail in orderDetails)
            {
                orderDetail.Product = productService.GetById(orderDetail.ProductId);
                totalPrice += orderDetail.Product.Price * orderDetail.Quantity;
            }
            return totalPrice;
        }

        public OrderDetail? IsQuantityPositive(int productId, int quantity)
        {
            Product product = productService.GetById(productId);
            if (quantity <= 0)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.Product = product;
                return orderDetail;
            }
            return null;
        }

        public IEnumerable<OrderDetail> AddProduct(IEnumerable<OrderDetail> orderDetails)
        {
            foreach (OrderDetail orderDetail in orderDetails)
            {
                orderDetail.Product = productService.GetById(orderDetail.ProductId);
            }
            return orderDetails;
        }
    }
}
