using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services
{
    public class ReviewService(
        IReviewRepository reviewRepository,
        IOrderRepository orderRepository,
        ApplicationDbContext dbContext) : IReviewService
    {
        private readonly IReviewRepository _reviewRepository = reviewRepository;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly ApplicationDbContext _dbContext = dbContext;

        // Public
        public async Task<IEnumerable<Review>> GetApprovedByProductIdAsync(int productId)
        {
            return await _dbContext.Reviews
                .Include(r => r.User)
                .Where(r => r.ProductId == productId && r.IsApproved)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<double> GetAverageRatingAsync(int productId)
        {
            return await _reviewRepository.GetAverageRatingByProductIdAsync(productId);
        }

        // Customer
        public async Task<Review> AddReviewAsync(int userId, int productId, int rating, string? comment)
        {
            // Check if user can review (has purchased)
            if (!await CanReviewAsync(userId, productId))
            {
                throw new Exception("You can only review products you have purchased");
            }

            // Check if user has already reviewed this product
            var existingReview = await _dbContext.Reviews
                .FirstOrDefaultAsync(r => r.UserId == userId && r.ProductId == productId);

            if (existingReview != null)
            {
                throw new Exception("You have already reviewed this product");
            }

            var review = new Review
            {
                UserId = userId,
                ProductId = productId,
                Rating = Math.Clamp(rating, 1, 5),
                Comment = comment,
                IsApproved = false, // Requires staff approval
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _reviewRepository.AddAsync(review);
            await _dbContext.SaveChangesAsync();

            return review;
        }

        public async Task UpdateReviewAsync(int userId, int reviewId, int rating, string? comment)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId)
                ?? throw new Exception("Review not found");

            if (review.UserId != userId)
            {
                throw new Exception("Unauthorized");
            }

            review.Rating = Math.Clamp(rating, 1, 5);
            review.Comment = comment;
            review.IsApproved = false; // Re-approve after edit
            review.UpdatedAt = DateTime.UtcNow;

            await _reviewRepository.UpdateAsync(review);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int userId, int reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId)
                ?? throw new Exception("Review not found");

            if (review.UserId != userId)
            {
                throw new Exception("Unauthorized");
            }

            await _reviewRepository.DeleteAsync(review);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Review>> GetByUserIdAsync(int userId)
        {
            return await _reviewRepository.GetByUserIdAsync(userId);
        }

        public async Task<bool> CanReviewAsync(int userId, int productId)
        {
            // Check if user has a delivered order containing this product
            var orders = await _orderRepository.GetByUserIdAndProductIdAsync(userId, productId);
            return orders.Any(o => o.Status == OrderStatus.Delivered);
        }

        // Staff+
        public async Task<IEnumerable<Review>> GetPendingReviewsAsync()
        {
            return await _dbContext.Reviews
                .Include(r => r.User)
                .Include(r => r.Product)
                .Where(r => !r.IsApproved)
                .OrderBy(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task ApproveReviewAsync(int reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId)
                ?? throw new Exception("Review not found");

            review.IsApproved = true;
            review.UpdatedAt = DateTime.UtcNow;

            await _reviewRepository.UpdateAsync(review);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RejectReviewAsync(int reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId)
                ?? throw new Exception("Review not found");

            await _reviewRepository.DeleteAsync(review);
            await _dbContext.SaveChangesAsync();
        }
    }
}
