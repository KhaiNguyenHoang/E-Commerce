using E_Commerce.Models;

namespace E_Commerce.Repositories
{
    public interface IWishlistItemRepository
    {
        Task<WishlistItem?> GetByIdAsync(int id);
        Task<IEnumerable<WishlistItem>> GetByWishlistIdAsync(int wishlistId);
        Task<bool> ExistsAsync(int wishlistId, int productId);
        Task<WishlistItem?> GetByWishlistIdAndProductIdAsync(int wishlistId, int productId);
        Task AddAsync(WishlistItem wishlistItem);
        Task DeleteAsync(WishlistItem wishlistItem);
    }
}
