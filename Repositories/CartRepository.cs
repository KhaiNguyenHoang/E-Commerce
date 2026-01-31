using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories
{
    public class CartRepository(ApplicationDbContext dbContext) : ICartRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Cart?> GetByIdAsync(int id)
        {
            return await _dbContext.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariant)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cart?> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Cart?> GetByUserIdWithItemsAsync(int userId)
        {
            return await _dbContext.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariant)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task AddAsync(Cart cart)
        {
            cart.CreatedAt = DateTime.UtcNow;
            cart.UpdatedAt = DateTime.UtcNow;
            await _dbContext.Carts.AddAsync(cart);
        }

        public async Task DeleteAsync(Cart cart)
        {
            var cartToDelete = await _dbContext.Carts.FindAsync(cart.Id)
                ?? throw new Exception("Cart not found");
            _dbContext.Carts.Remove(cartToDelete);
        }
    }
}
