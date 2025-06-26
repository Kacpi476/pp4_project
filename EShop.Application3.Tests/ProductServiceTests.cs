using EShop.Application.Service;
using EShop.Domain.Models;
using EShop.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EShop.Application3.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly Mock<ILogger<ProductService>> _mockLogger;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _mockLogger = new Mock<ILogger<ProductService>>();
            _service = new ProductService(_mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnProducts()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Test Product 1", Price = 10.99m },
                new Product { Id = 2, Name = "Test Product 2", Price = 20.99m }
            };

            _mockRepository.Setup(r => r.GetProductsAsync())
                .ReturnsAsync(expectedProducts);

            // Act
            var result = await _service.GetProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProducts.Count, result.Count);
            Assert.Equal(expectedProducts[0].Name, result[0].Name);
        }

        [Fact]
        public async Task GetProductByIdAsync_WithValidId_ShouldReturnProduct()
        {
            // Arrange
            var productId = 1;
            var expectedProduct = new Product { Id = productId, Name = "Test Product", Price = 10.99m };

            _mockRepository.Setup(r => r.GetProductByIdAsync(productId))
                .ReturnsAsync(expectedProduct);

            // Act
            var result = await _service.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct.Id, result.Id);
            Assert.Equal(expectedProduct.Name, result.Name);
        }

        [Fact]
        public async Task GetProductByIdAsync_WithInvalidId_ShouldThrowArgumentException()
        {
            // Arrange
            var invalidId = 0;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _service.GetProductByIdAsync(invalidId));
        }

        [Fact]
        public async Task AddProductAsync_WithValidProduct_ShouldReturnProduct()
        {
            // Arrange
            var product = new Product
            {
                Name = "Valid Product",
                Price = 15.99m,
                StockQuantity = 10
            };

            var expectedProduct = new Product
            {
                Id = 1,
                Name = product.Name,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };

            _mockRepository.Setup(r => r.AddProductAsync(It.IsAny<Product>()))
                .ReturnsAsync(expectedProduct);

            // Act
            var result = await _service.AddProductAsync(product);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct.Name, result.Name);
            Assert.Equal(expectedProduct.Price, result.Price);
        }

        [Fact]
        public async Task AddProductAsync_WithNullProduct_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => 
                _service.AddProductAsync(null!));
        }

        [Fact]
        public async Task AddProductAsync_WithEmptyName_ShouldThrowArgumentException()
        {
            // Arrange
            var product = new Product
            {
                Name = "",
                Price = 15.99m
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _service.AddProductAsync(product));
        }

        [Fact]
        public async Task AddProductAsync_WithInvalidPrice_ShouldThrowArgumentException()
        {
            // Arrange
            var product = new Product
            {
                Name = "Valid Product",
                Price = 0
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _service.AddProductAsync(product));
        }

        [Fact]
        public async Task AddProductAsync_WithNegativeStock_ShouldThrowArgumentException()
        {
            // Arrange
            var product = new Product
            {
                Name = "Valid Product",
                Price = 15.99m,
                StockQuantity = -1
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _service.AddProductAsync(product));
        }

        [Fact]
        public async Task UpdateProductAsync_WithValidProduct_ShouldReturnUpdatedProduct()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Updated Product",
                Price = 25.99m,
                StockQuantity = 20
            };

            _mockRepository.Setup(r => r.UpdateProductAsync(It.IsAny<Product>()))
                .ReturnsAsync(product);

            // Act
            var result = await _service.UpdateProductAsync(product);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Price, result.Price);
        }

        [Fact]
        public async Task DeleteProductAsync_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            var productId = 1;
            _mockRepository.Setup(r => r.DeleteProductAsync(productId))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteProductAsync(productId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteProductAsync_WithInvalidId_ShouldThrowArgumentException()
        {
            // Arrange
            var invalidId = 0;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _service.DeleteProductAsync(invalidId));
        }
    }
} 