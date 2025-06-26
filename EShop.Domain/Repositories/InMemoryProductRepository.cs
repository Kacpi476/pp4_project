using EShop.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Domain.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new();
        private int _nextId = 1;

        public InMemoryProductRepository()
        {
            // Initialize with some sample products
            var sampleProducts = new List<Product>
            {
                new Product { Name = "Laptop", Price = 999.99m, Description = "High-performance laptop", StockQuantity = 10, CategoryId = 1 },
                new Product { Name = "Mouse", Price = 29.99m, Description = "Wireless mouse", StockQuantity = 50, CategoryId = 1 },
                new Product { Name = "Keyboard", Price = 79.99m, Description = "Mechanical keyboard", StockQuantity = 25, CategoryId = 1 },
                new Product { Name = "Monitor", Price = 299.99m, Description = "27-inch 4K monitor", StockQuantity = 15, CategoryId = 1 },
                new Product { Name = "Headphones", Price = 149.99m, Description = "Noise-cancelling headphones", StockQuantity = 30, CategoryId = 2 }
            };

            foreach (var product in sampleProducts)
            {
                product.Id = _nextId++;
                product.CreatedAt = DateTime.UtcNow;
                product.UpdatedAt = DateTime.UtcNow;
                _products.Add(product);
            }
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return Task.FromResult(_products.Where(p => !p.IsDeleted).ToList());
        }

        public Task<Product?> GetProductByIdAsync(int id)
        {
            return Task.FromResult(_products.FirstOrDefault(p => p.Id == id && !p.IsDeleted));
        }

        public Task<Product> AddProductAsync(Product product)
        {
            product.Id = _nextId++;
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;
            _products.Add(product);
            return Task.FromResult(product);
        }

        public Task<Product?> UpdateProductAsync(Product product)
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id && !p.IsDeleted);
            if (existing == null) return Task.FromResult<Product?>(null);
            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Description = product.Description;
            existing.StockQuantity = product.StockQuantity;
            existing.CategoryId = product.CategoryId;
            existing.UpdatedAt = DateTime.UtcNow;
            return Task.FromResult(existing);
        }

        public Task<bool> DeleteProductAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
            if (product == null) return Task.FromResult(false);
            product.IsDeleted = true;
            product.UpdatedAt = DateTime.UtcNow;
            return Task.FromResult(true);
        }

        public Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return Task.FromResult(_products.Where(p => p.CategoryId == categoryId && !p.IsDeleted).ToList());
        }

        public Task<bool> UpdateStockAsync(int productId, int quantity)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId && !p.IsDeleted);
            if (product == null || product.StockQuantity < quantity) return Task.FromResult(false);
            product.StockQuantity -= quantity;
            product.UpdatedAt = DateTime.UtcNow;
            return Task.FromResult(true);
        }
    }
} 