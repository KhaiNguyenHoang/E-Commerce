using E_Commerce.Models;

namespace E_Commerce.Services
{
    public interface ICartService
    {
        Task<Cart?> GetCartAsync(int userId);
        Task<CartItem> AddItemAsync(int userId, int productId, int? variantId, int quantity);
        Task UpdateItemQuantityAsync(int userId, int cartItemId, int quantity);
        Task RemoveItemAsync(int userId, int cartItemId);
        Task ClearCartAsync(int userId);
        Task<decimal> GetCartTotalAsync(int userId);
        Task<int> GetCartItemCountAsync(int userId);
    }
}
