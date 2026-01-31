using E_Commerce.Models;

namespace E_Commerce.Repositories
{
    public interface ICartRepository
    {
        Task<Cart?> GetByIdAsync(int id);
        Task<Cart?> GetByUserIdAsync(int userId);
        Task<Cart?> GetByUserIdWithItemsAsync(int userId);
        Task AddAsync(Cart cart);
        Task DeleteAsync(Cart cart);
    }
}
