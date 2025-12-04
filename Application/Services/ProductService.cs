using PizzaStore.Application.Interfaces;
using PizzaStore.Domain.Entities;
using PizzaStore.Infrastructure.Interfaces;

namespace PizzaStore.Application.Services
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        public void AddProduct(Product product)
        {
            productRepository.AddProduct(product);
        }

        public void DeleteProduct(int Id)
        {
            productRepository.DeleteProduct(Id);
        }
        public void UpdateProduct(Product product)
        {
            productRepository.UpdateProduct(product);
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return productRepository.GetAllProducts();
        }

        public Product GetById(int id)
        {
            Product product = productRepository.GetAllProducts()
                .Where(p => p.Id == id)
                .First();

            return product;
        }


    }
}
