using E_Commerce.Models;

namespace E_Commerce.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(int id);
        Task<Order> GetByNumberAsync(string number);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Order>> GetByUserIdAndProductIdAsync(int userId, int productId);
        Task<IEnumerable<Order>> GetByUserIdAndProductIdAndProductVariantIdAsync(int userId, int productId, int productVariantId);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);
    }
}
