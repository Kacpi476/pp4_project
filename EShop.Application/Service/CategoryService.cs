using EShop.Domain.Models;
using EShop.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace EShop.Application.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            try
            {
                return await _categoryRepository.GetCategoriesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting categories");
                throw;
            }
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Category ID must be greater than 0");

                return await _categoryRepository.GetCategoryByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting category with ID {CategoryId}", id);
                throw;
            }
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            try
            {
                if (category == null)
                    throw new ArgumentNullException(nameof(category));

                if (string.IsNullOrWhiteSpace(category.Name))
                    throw new ArgumentException("Category name cannot be null or empty");

                return await _categoryRepository.AddCategoryAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding category {CategoryName}", category?.Name);
                throw;
            }
        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            try
            {
                if (category == null)
                    throw new ArgumentNullException(nameof(category));

                if (category.Id <= 0)
                    throw new ArgumentException("Category ID must be greater than 0");

                if (string.IsNullOrWhiteSpace(category.Name))
                    throw new ArgumentException("Category name cannot be null or empty");

                return await _categoryRepository.UpdateCategoryAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating category with ID {CategoryId}", category?.Id);
                throw;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Category ID must be greater than 0");

                return await _categoryRepository.DeleteCategoryAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting category with ID {CategoryId}", id);
                throw;
            }
        }
    }
} 