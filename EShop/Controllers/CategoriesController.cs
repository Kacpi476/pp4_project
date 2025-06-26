using EShop.Application.Service;
using EShop.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _categoryService.GetCategoriesAsync();
                return Ok(new { success = true, data = categories });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting categories");
                return StatusCode(500, new { success = false, message = "An error occurred while retrieving categories" });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { success = false, message = "Invalid category ID" });

                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                    return NotFound(new { success = false, message = "Category not found" });

                return Ok(new { success = true, data = category });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting category with ID {CategoryId}", id);
                return StatusCode(500, new { success = false, message = "An error occurred while retrieving the category" });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { success = false, message = "Invalid category data", errors = ModelState });

                var newCategory = await _categoryService.AddCategoryAsync(category);
                return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.Id }, 
                    new { success = true, data = newCategory, message = "Category created successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating category");
                return StatusCode(500, new { success = false, message = "An error occurred while creating the category" });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            try
            {
                if (id != category.Id)
                    return BadRequest(new { success = false, message = "Category ID mismatch" });

                if (!ModelState.IsValid)
                    return BadRequest(new { success = false, message = "Invalid category data", errors = ModelState });

                var updatedCategory = await _categoryService.UpdateCategoryAsync(category);
                if (updatedCategory == null)
                    return NotFound(new { success = false, message = "Category not found" });

                return Ok(new { success = true, data = updatedCategory, message = "Category updated successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating category with ID {CategoryId}", id);
                return StatusCode(500, new { success = false, message = "An error occurred while updating the category" });
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { success = false, message = "Invalid category ID" });

                var deleted = await _categoryService.DeleteCategoryAsync(id);
                if (!deleted)
                    return NotFound(new { success = false, message = "Category not found" });

                return Ok(new { success = true, message = "Category deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting category with ID {CategoryId}", id);
                return StatusCode(500, new { success = false, message = "An error occurred while deleting the category" });
            }
        }
    }
} 