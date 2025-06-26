using System.Threading.Tasks;
using EShop.Application.Service;
using EShop.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using EShop.Controllers;
using Microsoft.Extensions.Configuration;

namespace EShop.Application3.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IUserService> _mockUserService = new();
        private readonly Mock<IClientService> _mockClientService = new();
        private readonly Mock<ILogger<AuthController>> _mockLogger = new();
        private readonly Mock<Microsoft.Extensions.Configuration.IConfiguration> _mockConfig = new();
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            // Create a real JwtService with test configuration
            var testConfig = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"JwtSettings:SecretKey", "supersecretkeysupersecretkey123456"},
                    {"JwtSettings:Issuer", "TestIssuer"},
                    {"JwtSettings:Audience", "TestAudience"},
                    {"JwtSettings:ExpirationInMinutes", "60"}
                })
                .Build();
            
            var jwtService = new JwtService(testConfig);
            
            _controller = new AuthController(
                _mockUserService.Object,
                _mockClientService.Object,
                jwtService,
                _mockLogger.Object,
                _mockConfig.Object);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
        {
            _mockUserService.Setup(s => s.GetUserByUsernameAsync("user")).ReturnsAsync((User?)null);
            var request = new LoginRequest { Username = "user", Password = "wrong" };
            var result = await _controller.Login(request);
            Assert.IsType<UnauthorizedObjectResult>(result);
        }
    }
} 