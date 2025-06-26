using EShop.Domain.Models;

namespace EShop.Domain.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product> AddProductAsync(Product product);
    Task<Product?> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(int id);
    Task<List<Product>> GetProductsByCategoryAsync(int categoryId);
    Task<bool> UpdateStockAsync(int productId, int quantity);
}