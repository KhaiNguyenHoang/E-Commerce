using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repositories;

namespace E_Commerce.Services
{
    public class CategoryService(
        ICategoryRepository categoryRepository,
        ApplicationDbContext dbContext) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Category>> GetActiveAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        // Admin only
        public async Task<Category> CreateAsync(string name, string? description)
        {
            if (await _categoryRepository.ExistsByNameAsync(name))
            {
                throw new Exception("Category name already exists");
            }

            var category = new Category
            {
                Name = name,
                Description = description,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _categoryRepository.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task UpdateAsync(int id, string name, string? description, bool isActive)
        {
            var category = await _categoryRepository.GetByIdAsync(id)
                ?? throw new Exception("Category not found");

            // Check if name is taken by another category
            var existingCategory = await _categoryRepository.GetByNameAsync(name);
            if (existingCategory != null && existingCategory.Id != id)
            {
                throw new Exception("Category name already exists");
            }

            category.Name = name;
            category.Description = description;
            category.IsActive = isActive;
            category.UpdatedAt = DateTime.UtcNow;

            await _categoryRepository.UpdateAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id)
                ?? throw new Exception("Category not found");

            await _categoryRepository.DeleteAsync(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}
