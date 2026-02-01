using E_Commerce.Models;

namespace E_Commerce.Services
{
    public interface IReviewService
    {
        // Public
        Task<IEnumerable<Review>> GetApprovedByProductIdAsync(int productId);
        Task<double> GetAverageRatingAsync(int productId);
        
        // Customer
        Task<Review> AddReviewAsync(int userId, int productId, int rating, string? comment);
        Task UpdateReviewAsync(int userId, int reviewId, int rating, string? comment);
        Task DeleteReviewAsync(int userId, int reviewId);
        Task<IEnumerable<Review>> GetByUserIdAsync(int userId);
        Task<bool> CanReviewAsync(int userId, int productId);
        
        // Staff+
        Task<IEnumerable<Review>> GetPendingReviewsAsync();
        Task ApproveReviewAsync(int reviewId);
        Task RejectReviewAsync(int reviewId);
    }
}
