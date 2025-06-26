using System.Threading.Tasks;
using EShop.Application.Service;
using EShop.Domain.Models;
using EShop.Domain.Repositories;
using Moq;
using Xunit;

namespace EShop.Application3.Tests
{
    public class CartServiceTests
    {
        private readonly Mock<ICartRepository> _mockCartRepo = new();
        private readonly Mock<IProductRepository> _mockProductRepo = new();
        private readonly CartService _service;

        public CartServiceTests()
        {
            _service = new CartService(_mockCartRepo.Object, _mockProductRepo.Object);
        }

        [Fact]
        public async Task AddToCart_ShouldCallRepository()
        {
            var product = new Product { Id = 1, Name = "Test Product", Price = 10.99m };
            _mockProductRepo.Setup(r => r.GetProductByIdAsync(1)).ReturnsAsync(product);
            
            await _service.AddToCart(1, 1, 2);
            
            _mockCartRepo.Verify(r => r.AddToCart(1, It.IsAny<CartItem>()), Times.Once);
        }

        [Fact]
        public void GetCart_ShouldReturnCart()
        {
            var cart = new Cart { Id = 1, ClientId = 1 };
            _mockCartRepo.Setup(r => r.GetCart(1)).Returns(cart);
            
            var result = _service.GetCart(1);
            
            Assert.Equal(cart, result);
        }
    }
} 