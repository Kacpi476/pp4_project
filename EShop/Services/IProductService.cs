using EShop.Models;

namespace EShop.Services;

public interface IProductService
{
    List<Product> GetProducts();
    Product? GetProductById(int id);
    Product AddProduct(Product product);
}