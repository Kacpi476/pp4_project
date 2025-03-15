using EShop.Models;

namespace EShop.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();

    public ProductRepository()
    {
        _products.Add(new Product{ Id = 1, Name = "Apple", Price = 10 });
        _products.Add(new Product{ Id = 2, Name = "Orange", Price = 20 });
    }
    
    public List<Product> GetProducts() => _products;
    
    public  Product? GetProductById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public Product AddProduct(Product product)
    {
        product.Id = _products.Count + 1;
        _products.Add(product);
        return product;
    }
}