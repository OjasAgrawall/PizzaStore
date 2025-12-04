using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Entities;
using PizzaStore.Infrastructure.Data;
using PizzaStore.Infrastructure.Interfaces;

namespace PizzaStore.Infrastructure.ModelBusinessLayer
{
    public class ProductRepository(PizzaContext context) : IProductRepository
    {
        public void AddProduct(Product products)
        {
            context.Database.ExecuteSqlInterpolated($"EXECUTE spAddProduct {products.Name}, {products.Price}, {products.Descriptions}");
        }

        public void UpdateProduct(Product products)
        {
            context.Database.ExecuteSqlInterpolated($"EXECUTE spUpdateProducts {products.Id}, {products.Name}, {products.Price}, {products.Descriptions}");
        }

        public void DeleteProduct(int id)
        {
            context.Database.ExecuteSqlInterpolated($"EXECUTE spDeleteProduct {id}");
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return context.Products;
        }
    }
}
