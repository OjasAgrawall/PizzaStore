using PizzaStore.Application.Interfaces;
using PizzaStore.Domain.Entities;
using PizzaStore.Infrastructure.Interfaces;

namespace PizzaStore.Application.Services
{
    public class OrderService(IOrderRepository orderRepo) : IOrderService
    {
        void IOrderRepository.AddCustomerId(int CustomerId)
        {
            orderRepo.AddCustomerId(CustomerId);
        }

        void IOrderRepository.AddOrderPlaced(int id, DateTime orderPlaced)
        {
            orderRepo.AddOrderPlaced(id, orderPlaced);
        }

        public Order GetById(int id)
        {
            return orderRepo.GetById(id);
        }

        public Order GetByCustomerId(int id)
        {
            return orderRepo.GetByCustomerId(id);
        }
    }
}
