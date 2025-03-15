using EShop.Models;

namespace EShop.Repositories;

public interface IProductRepository
{
    List<Product> GetProducts();
    Product? GetProductById(int id);
    Product AddProduct(Product product);
}