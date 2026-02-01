using E_Commerce.Models;

namespace E_Commerce.Services
{
    public interface IWishlistService
    {
        Task<Wishlist?> GetWishlistAsync(int userId);
        Task<WishlistItem> AddItemAsync(int userId, int productId);
        Task RemoveItemAsync(int userId, int productId);
        Task<bool> IsInWishlistAsync(int userId, int productId);
        Task MoveToCartAsync(int userId, int productId, int? variantId, int quantity);
        Task<int> GetWishlistItemCountAsync(int userId);
    }
}
