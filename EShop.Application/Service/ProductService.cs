using EShop.Domain.Models;
using EShop.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace EShop.Application.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }
    
    public async Task<List<Product>> GetProductsAsync()
    {
        try
        {
            return await _productRepository.GetProductsAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting products");
            throw;
        }
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        try
        {
            if (id <= 0)
                throw new ArgumentException("Product ID must be greater than 0");

            return await _productRepository.GetProductByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting product with ID {ProductId}", id);
            throw;
        }
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        try
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name cannot be null or empty");

            if (product.Price <= 0)
                throw new ArgumentException("Product price must be greater than 0");

            if (product.StockQuantity < 0)
                throw new ArgumentException("Product stock quantity cannot be negative");

            return await _productRepository.AddProductAsync(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding product {ProductName}", product?.Name);
            throw;
        }
    }

    public async Task<Product?> UpdateProductAsync(Product product)
    {
        try
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (product.Id <= 0)
                throw new ArgumentException("Product ID must be greater than 0");

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name cannot be null or empty");

            if (product.Price <= 0)
                throw new ArgumentException("Product price must be greater than 0");

            if (product.StockQuantity < 0)
                throw new ArgumentException("Product stock quantity cannot be negative");

            return await _productRepository.UpdateProductAsync(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating product with ID {ProductId}", product?.Id);
            throw;
        }
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        try
        {
            if (id <= 0)
                throw new ArgumentException("Product ID must be greater than 0");

            return await _productRepository.DeleteProductAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting product with ID {ProductId}", id);
            throw;
        }
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        try
        {
            if (categoryId <= 0)
                throw new ArgumentException("Category ID must be greater than 0");

            return await _productRepository.GetProductsByCategoryAsync(categoryId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting products for category {CategoryId}", categoryId);
            throw;
        }
    }
}