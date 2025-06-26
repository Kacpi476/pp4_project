using System.Threading.Tasks;
using EShop.Application.Service;
using EShop.Domain.Models;
using EShop.Domain.Repositories;
using Moq;
using Xunit;

namespace EShop.Application3.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockRepo = new();
        private readonly UserService _service;

        public UserServiceTests()
        {
            _service = new UserService(_mockRepo.Object);
        }

        [Fact]
        public async Task AddUserAsync_ShouldCallRepository()
        {
            var user = new User { Id = 1, Username = "user", PasswordHash = "hash" };
            _mockRepo.Setup(r => r.AddUserAsync(user)).ReturnsAsync(user);
            var result = await _service.AddUserAsync(user);
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task GetUserByUsernameAsync_ShouldReturnUser()
        {
            var user = new User { Id = 1, Username = "user" };
            _mockRepo.Setup(r => r.GetUserByUsernameAsync("user")).ReturnsAsync(user);
            var result = await _service.GetUserByUsernameAsync("user");
            Assert.Equal(user, result);
        }
    }
} 