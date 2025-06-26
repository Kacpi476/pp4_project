using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Xunit;
using EShop.Application.Service;

namespace EShop.Application3.Tests
{
    public class JwtServiceTests
    {
        private readonly JwtService _jwtService;
        private readonly IConfiguration _configuration;

        public JwtServiceTests()
        {
            var inMemorySettings = new Dictionary<string, string?>
            {
                {"JwtSettings:SecretKey", "supersecretkeysupersecretkey123456"},
                {"JwtSettings:Issuer", "TestIssuer"},
                {"JwtSettings:Audience", "TestAudience"},
                {"JwtSettings:ExpirationInMinutes", "60"}
            };
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            _jwtService = new JwtService(_configuration);
        }

        [Fact]
        public void GenerateToken_ShouldReturnValidJwt()
        {
            var token = _jwtService.GenerateToken("testuser", "test@example.com", 1);
            Assert.False(string.IsNullOrWhiteSpace(token));
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            Assert.Equal("TestIssuer", jwt.Issuer);
            Assert.Equal("TestAudience", jwt.Audiences.First());
        }

        [Fact]
        public void ValidateToken_ShouldReturnClaimsPrincipal()
        {
            var token = _jwtService.GenerateToken("testuser", "test@example.com", 1);
            var principal = _jwtService.ValidateToken(token);
            Assert.NotNull(principal);
            Assert.True(principal.Identity?.IsAuthenticated);
            Assert.Equal("testuser", principal.FindFirst(ClaimTypes.Name)?.Value);
            Assert.Equal("test@example.com", principal.FindFirst(ClaimTypes.Email)?.Value);
            Assert.Equal("1", principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        [Fact]
        public void ValidateToken_WithInvalidToken_ShouldReturnNull()
        {
            var invalidToken = "invalid.token.value";
            var principal = _jwtService.ValidateToken(invalidToken);
            Assert.Null(principal);
        }
    }
} 