using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories
{
    public class CategoryRepository(ApplicationDbContext dbContext) : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task AddAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
        }

        public async Task DeleteAsync(Category category)
        {
            var categoryToDelete = await _dbContext.Categories.FindAsync(category.Id)
                 ?? throw new Exception("Category not found");
            categoryToDelete.IsActive = false;
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _dbContext.Categories.AnyAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task UpdateAsync(Category category)
        {
            var categoryToUpdate = await _dbContext.Categories.FindAsync(category.Id)
                ?? throw new Exception("Category not found");
            categoryToUpdate.Name = category.Name;
            categoryToUpdate.Description = category.Description;
            categoryToUpdate.IsActive = category.IsActive;
            categoryToUpdate.UpdatedAt = DateTime.UtcNow;
        }
    }
}
