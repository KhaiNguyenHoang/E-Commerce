using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repositories;

namespace E_Commerce.Services
{
    public class UserService(
        IUserRepository userRepository,
        ApplicationDbContext dbContext) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task UpdateProfileAsync(int userId, string fullName, string? phoneNumber, string? avatarUrl)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new Exception("User not found");

            user.FullName = fullName;
            user.PhoneNumber = phoneNumber;
            user.AvatarUrl = avatarUrl;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new Exception("User not found");

            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.Password))
            {
                throw new Exception("Current password is incorrect");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        // Admin only
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task SetActiveStatusAsync(int userId, bool isActive)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new Exception("User not found");

            user.IsActive = isActive;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
