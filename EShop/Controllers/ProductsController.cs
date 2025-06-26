using EShop.Application.Service;
using EShop.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        public class ProductDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
            public decimal Price { get; set; }
            public int StockQuantity { get; set; }
            public int? CategoryId { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _productService.GetProductsAsync();
                var productDtos = products.Select(p => new ProductDto {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId
                }).ToList();
                return Ok(new { success = true, data = productDtos });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products");
                return StatusCode(500, new { success = false, message = "An error occurred while retrieving products" });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { success = false, message = "Invalid product ID" });

                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                    return NotFound(new { success = false, message = "Product not found" });

                var productDto = new ProductDto {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    StockQuantity = product.StockQuantity,
                    CategoryId = product.CategoryId
                };
                return Ok(new { success = true, data = productDto });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting product with ID {ProductId}", id);
                return StatusCode(500, new { success = false, message = "An error occurred while retrieving the product" });
            }
        }

        [HttpGet("category/{categoryId:int}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            try
            {
                if (categoryId <= 0)
                    return BadRequest(new { success = false, message = "Invalid category ID" });

                var products = await _productService.GetProductsByCategoryAsync(categoryId);
                var productDtos = products.Select(p => new ProductDto {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId
                }).ToList();
                return Ok(new { success = true, data = productDtos });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products for category {CategoryId}", categoryId);
                return StatusCode(500, new { success = false, message = "An error occurred while retrieving products" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { success = false, message = "Invalid product data", errors = ModelState });

                var newProduct = await _productService.AddProductAsync(product);
                return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, 
                    new { success = true, data = newProduct, message = "Product created successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating product");
                return StatusCode(500, new { success = false, message = "An error occurred while creating the product" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                if (id != product.Id)
                    return BadRequest(new { success = false, message = "Product ID mismatch" });

                if (!ModelState.IsValid)
                    return BadRequest(new { success = false, message = "Invalid product data", errors = ModelState });

                var updatedProduct = await _productService.UpdateProductAsync(product);
                if (updatedProduct == null)
                    return NotFound(new { success = false, message = "Product not found" });

                return Ok(new { success = true, data = updatedProduct, message = "Product updated successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product with ID {ProductId}", id);
                return StatusCode(500, new { success = false, message = "An error occurred while updating the product" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { success = false, message = "Invalid product ID" });

                var deleted = await _productService.DeleteProductAsync(id);
                if (!deleted)
                    return NotFound(new { success = false, message = "Product not found" });

                return Ok(new { success = true, message = "Product deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting product with ID {ProductId}", id);
                return StatusCode(500, new { success = false, message = "An error occurred while deleting the product" });
            }
        }
    }
}