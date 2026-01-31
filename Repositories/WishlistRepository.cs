using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories
{
    public class WishlistRepository(ApplicationDbContext dbContext) : IWishlistRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Wishlist?> GetByIdAsync(int id)
        {
            return await _dbContext.Wishlists
                .Include(w => w.WishlistItems)
                    .ThenInclude(wi => wi.Product)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Wishlist?> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task<Wishlist?> GetByUserIdWithItemsAsync(int userId)
        {
            return await _dbContext.Wishlists
                .Include(w => w.WishlistItems)
                    .ThenInclude(wi => wi.Product)
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task AddAsync(Wishlist wishlist)
        {
            wishlist.CreatedAt = DateTime.UtcNow;
            wishlist.UpdatedAt = DateTime.UtcNow;
            await _dbContext.Wishlists.AddAsync(wishlist);
        }

        public async Task DeleteAsync(Wishlist wishlist)
        {
            var wishlistToDelete = await _dbContext.Wishlists.FindAsync(wishlist.Id) 
                ?? throw new Exception("Wishlist not found");
            _dbContext.Wishlists.Remove(wishlistToDelete);
        }
    }
}
