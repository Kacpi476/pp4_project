using EShop.Application.Service;
using EShop.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;

namespace EShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IClientService _clientService;
        private readonly JwtService _jwtService;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;

        public AuthController(
            IUserService userService, 
            IClientService clientService, 
            JwtService jwtService, 
            ILogger<AuthController> logger,
            IConfiguration configuration)
        {
            _userService = userService;
            _clientService = clientService;
            _jwtService = jwtService;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { success = false, message = "Invalid request data", errors = ModelState });

                // Check if user already exists
                var existingUser = await _userService.GetUserByUsernameAsync(request.Username);
                if (existingUser != null)
                    return BadRequest(new { success = false, message = "Username already exists" });

                // Hash password
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                // Create user
                var user = new User
                {
                    Username = request.Username,
                    PasswordHash = passwordHash,
                    Email = null,
                    IsActive = true
                };

                var createdUser = await _userService.AddUserAsync(user);

                // Generate token
                var token = _jwtService.GenerateToken(createdUser.Username, createdUser.Email ?? "", createdUser.Id);

                // Get expiration time from configuration
                var expirationMinutes = _configuration.GetValue<int>("JwtSettings:ExpirationInMinutes", 60);
                var expirationTime = DateTime.UtcNow.AddMinutes(expirationMinutes);

                var response = new AuthResponse
                {
                    Token = token,
                    Username = createdUser.Username,
                    Email = createdUser.Email ?? "",
                    ExpiresAt = expirationTime
                };

                return Ok(new { success = true, data = response, message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user registration");
                return StatusCode(500, new { success = false, message = "An error occurred during registration" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { success = false, message = "Invalid request data", errors = ModelState });

                // Find user by username
                var user = await _userService.GetUserByUsernameAsync(request.Username);
                if (user == null)
                    return Unauthorized(new { success = false, message = "Invalid username or password" });

                // Verify password
                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                    return Unauthorized(new { success = false, message = "Invalid username or password" });

                if (!user.IsActive)
                    return Unauthorized(new { success = false, message = "Account is deactivated" });

                // Generate token
                var token = _jwtService.GenerateToken(user.Username, user.Email ?? "", user.Id);

                // Get expiration time from configuration
                var expirationMinutes = _configuration.GetValue<int>("JwtSettings:ExpirationInMinutes", 60);
                var expirationTime = DateTime.UtcNow.AddMinutes(expirationMinutes);

                var response = new AuthResponse
                {
                    Token = token,
                    Username = user.Username,
                    Email = user.Email ?? "",
                    ExpiresAt = expirationTime
                };

                return Ok(new { success = true, data = response, message = "Login successful" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user login");
                return StatusCode(500, new { success = false, message = "An error occurred during login" });
            }
        }

        [HttpGet("profile")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    return Unauthorized(new { success = false, message = "Invalid token" });

                var user = await _userService.GetUserByIdAsync(userId);
                if (user == null)
                    return NotFound(new { success = false, message = "User not found" });

                // Remove sensitive information
                var profileData = new
                {
                    user.Id,
                    user.Username,
                    user.Email,
                    user.FirstName,
                    user.LastName,
                    user.IsActive,
                    user.CreatedAt
                };

                return Ok(new { success = true, data = profileData });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user profile");
                return StatusCode(500, new { success = false, message = "An error occurred while retrieving profile" });
            }
        }

        [HttpGet("test-auth")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult TestAuth()
        {
            try
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                var usernameClaim = User.FindFirst(System.Security.Claims.ClaimTypes.Name);
                var emailClaim = User.FindFirst(System.Security.Claims.ClaimTypes.Email);

                var testData = new
                {
                    IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
                    UserId = userIdClaim?.Value,
                    Username = usernameClaim?.Value,
                    Email = emailClaim?.Value,
                    Claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList()
                };

                return Ok(new { success = true, data = testData, message = "Authentication test successful" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during authentication test");
                return StatusCode(500, new { success = false, message = "An error occurred during authentication test" });
            }
        }
    }
} 