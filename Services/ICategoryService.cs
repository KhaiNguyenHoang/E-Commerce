using E_Commerce.Models;

namespace E_Commerce.Services
{
    public interface ICategoryService
    {
        // Public
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<Category>> GetActiveAsync();
        Task<Category?> GetByIdAsync(int id);
        
        // Admin only
        Task<Category> CreateAsync(string name, string? description);
        Task UpdateAsync(int id, string name, string? description, bool isActive);
        Task DeleteAsync(int id);
    }
}
