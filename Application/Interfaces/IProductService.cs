using PizzaStore.Domain.Entities;
using PizzaStore.Infrastructure.Interfaces;

namespace PizzaStore.Application.Interfaces
{
    public interface IProductService : IProductRepository
    {
        public Product GetById(int id);
    }
}
