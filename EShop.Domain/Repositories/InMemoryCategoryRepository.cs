using EShop.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Domain.Repositories
{
    public class InMemoryCategoryRepository : ICategoryRepository
    {
        private readonly List<Category> _categories = new();
        private int _nextId = 1;

        public Task<List<Category>> GetCategoriesAsync()
        {
            return Task.FromResult(_categories.Where(c => !c.IsDeleted).ToList());
        }

        public Task<Category?> GetCategoryByIdAsync(int id)
        {
            return Task.FromResult(_categories.FirstOrDefault(c => c.Id == id && !c.IsDeleted));
        }

        public Task<Category> AddCategoryAsync(Category category)
        {
            category.Id = _nextId++;
            category.CreatedAt = DateTime.UtcNow;
            category.UpdatedAt = DateTime.UtcNow;
            _categories.Add(category);
            return Task.FromResult(category);
        }

        public Task<Category?> UpdateCategoryAsync(Category category)
        {
            var existing = _categories.FirstOrDefault(c => c.Id == category.Id && !c.IsDeleted);
            if (existing == null) return Task.FromResult<Category?>(null);
            existing.Name = category.Name;
            existing.Description = category.Description;
            existing.IsActive = category.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;
            return Task.FromResult(existing);
        }

        public Task<bool> DeleteCategoryAsync(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id && !c.IsDeleted);
            if (category == null) return Task.FromResult(false);
            category.IsDeleted = true;
            category.UpdatedAt = DateTime.UtcNow;
            return Task.FromResult(true);
        }
    }
} 