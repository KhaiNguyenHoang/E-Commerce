using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories
{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task DeleteAsync(User user)
        {
            var userToDelete = await _dbContext.Users.FindAsync(user.Id) 
                ?? throw new Exception("User not found");
            
            // Soft delete for User
            userToDelete.IsActive = false;
            userToDelete.UpdatedAt = DateTime.UtcNow;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _dbContext.Users
                .AnyAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users
                .ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateAsync(User user)
        {
            var userToUpdate = await _dbContext.Users.FindAsync(user.Id) 
                ?? throw new Exception("User not found");
            
            userToUpdate.FullName = user.FullName;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;
            userToUpdate.PhoneNumber = user.PhoneNumber;
            userToUpdate.AvatarUrl = user.AvatarUrl;
            userToUpdate.IsActive = user.IsActive;
            userToUpdate.LastLoginAt = user.LastLoginAt;
            userToUpdate.RoleId = user.RoleId;
            userToUpdate.UpdatedAt = DateTime.UtcNow;
        }
    }
}
