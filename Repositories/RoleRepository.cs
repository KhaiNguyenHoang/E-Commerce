using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories
{
    public class RoleRepository(ApplicationDbContext dbContext) : IRoleRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _dbContext.Roles.FindAsync(id);
        }

        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _dbContext.Roles.ToListAsync();
        }

        public async Task AddAsync(Role role)
        {
            role.CreatedAt = DateTime.UtcNow;
            role.UpdatedAt = DateTime.UtcNow;
            await _dbContext.Roles.AddAsync(role);
        }

        public async Task UpdateAsync(Role role)
        {
            var roleToUpdate = await _dbContext.Roles.FindAsync(role.Id) 
                ?? throw new Exception("Role not found");
            roleToUpdate.Name = role.Name;
            roleToUpdate.Description = role.Description;
            roleToUpdate.UpdatedAt = DateTime.UtcNow;
        }

        public async Task DeleteAsync(Role role)
        {
            var roleToDelete = await _dbContext.Roles.FindAsync(role.Id) 
                ?? throw new Exception("Role not found");
            _dbContext.Roles.Remove(roleToDelete);
        }
    }
}
