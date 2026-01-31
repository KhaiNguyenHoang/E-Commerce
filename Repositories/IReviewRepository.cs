using E_Commerce.Models;

namespace E_Commerce.Repositories
{
    public interface IReviewRepository
    {
        Task<Review?> GetByIdAsync(int id);
        Task<IEnumerable<Review>> GetByProductIdAsync(int productId);
        Task<IEnumerable<Review>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Review>> GetApprovedByProductIdAsync(int productId);
        Task<bool> ExistsAsync(int userId, int productId);
        Task<double> GetAverageRatingByProductIdAsync(int productId);
        Task AddAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(Review review);
    }
}
