using EShop.Models;
using EShop.Repositories;

namespace EShop.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public List<Product>  GetProducts() => _productRepository.GetProducts();
    public Product? GetProductById(int id) => _productRepository.GetProductById(id);

    public Product AddProduct(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name cannot be null or empty");
        return _productRepository.AddProduct(product);
    }
}