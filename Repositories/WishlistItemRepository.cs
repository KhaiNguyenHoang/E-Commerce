using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories
{
    public class WishlistItemRepository(ApplicationDbContext dbContext) : IWishlistItemRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<WishlistItem?> GetByIdAsync(int id)
        {
            return await _dbContext.WishlistItems
                .Include(wi => wi.Product)
                .FirstOrDefaultAsync(wi => wi.Id == id);
        }

        public async Task<IEnumerable<WishlistItem>> GetByWishlistIdAsync(int wishlistId)
        {
            return await _dbContext.WishlistItems
                .Include(wi => wi.Product)
                .Where(wi => wi.WishlistId == wishlistId)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int wishlistId, int productId)
        {
            return await _dbContext.WishlistItems
                .AnyAsync(wi => wi.WishlistId == wishlistId && wi.ProductId == productId);
        }

        public async Task<WishlistItem?> GetByWishlistIdAndProductIdAsync(int wishlistId, int productId)
        {
            return await _dbContext.WishlistItems
                .FirstOrDefaultAsync(wi => wi.WishlistId == wishlistId && wi.ProductId == productId);
        }

        public async Task AddAsync(WishlistItem wishlistItem)
        {
            wishlistItem.CreatedAt = DateTime.UtcNow;
            wishlistItem.UpdatedAt = DateTime.UtcNow;
            await _dbContext.WishlistItems.AddAsync(wishlistItem);
        }

        public async Task DeleteAsync(WishlistItem wishlistItem)
        {
            var itemToDelete = await _dbContext.WishlistItems.FindAsync(wishlistItem.Id) 
                ?? throw new Exception("Wishlist item not found");
            _dbContext.WishlistItems.Remove(itemToDelete);
        }
    }
}
