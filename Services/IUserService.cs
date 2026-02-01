using E_Commerce.Models;

namespace E_Commerce.Services
{
    public interface IUserService
    {
        // Customer: Own profile
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task UpdateProfileAsync(int userId, string fullName, string? phoneNumber, string? avatarUrl);
        Task ChangePasswordAsync(int userId, string currentPassword, string newPassword);
        
        // Admin only
        Task<IEnumerable<User>> GetAllAsync();
        Task SetActiveStatusAsync(int userId, bool isActive);
    }
}
