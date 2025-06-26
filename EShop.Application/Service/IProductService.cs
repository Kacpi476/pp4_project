using EShop.Domain.Models;

namespace EShop.Application.Service;

public interface IProductService
{
    Task<List<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product> AddProductAsync(Product product);
    Task<Product?> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(int id);
    Task<List<Product>> GetProductsByCategoryAsync(int categoryId);
}