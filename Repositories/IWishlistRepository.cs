using E_Commerce.Models;

namespace E_Commerce.Repositories
{
    public interface IWishlistRepository
    {
        Task<Wishlist?> GetByIdAsync(int id);
        Task<Wishlist?> GetByUserIdAsync(int userId);
        Task<Wishlist?> GetByUserIdWithItemsAsync(int userId);
        Task AddAsync(Wishlist wishlist);
        Task DeleteAsync(Wishlist wishlist);
    }
}
