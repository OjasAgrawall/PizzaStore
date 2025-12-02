using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PizzaStore.Domain.Entities;

namespace PizzaStore.Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        public void AddProduct(Product product);

        public void UpdateProduct(Product product);

        public void DeleteProduct(int Id);

        public IEnumerable<Product> GetAllProducts();
    }
}
