using E_Commerce.Models;

namespace E_Commerce.Repositories
{
    public interface ICartItemRepository
    {
        Task<CartItem?> GetByIdAsync(int id);
        Task<IEnumerable<CartItem>> GetByCartIdAsync(int cartId);
        Task<CartItem?> GetByCartIdAndProductIdAsync(int cartId, int productId, int? productVariantId);
        Task AddAsync(CartItem cartItem);
        Task UpdateAsync(CartItem cartItem);
        Task DeleteAsync(CartItem cartItem);
        Task ClearCartAsync(int cartId);
    }
}
