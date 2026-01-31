using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories
{
    public class ReviewRepository(ApplicationDbContext dbContext) : IReviewRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _dbContext.Reviews
                .Include(r => r.User)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Review>> GetByProductIdAsync(int productId)
        {
            return await _dbContext.Reviews
                .Include(r => r.User)
                .Where(r => r.ProductId == productId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Reviews
                .Include(r => r.Product)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetApprovedByProductIdAsync(int productId)
        {
            return await _dbContext.Reviews
                .Include(r => r.User)
                .Where(r => r.ProductId == productId && r.IsApproved)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int userId, int productId)
        {
            return await _dbContext.Reviews
                .AnyAsync(r => r.UserId == userId && r.ProductId == productId);
        }

        public async Task<double> GetAverageRatingByProductIdAsync(int productId)
        {
            var reviews = await _dbContext.Reviews
                .Where(r => r.ProductId == productId && r.IsApproved)
                .ToListAsync();
            
            if (reviews.Count == 0)
                return 0;
                
            return reviews.Average(r => r.Rating);
        }

        public async Task AddAsync(Review review)
        {
            review.CreatedAt = DateTime.UtcNow;
            review.UpdatedAt = DateTime.UtcNow;
            await _dbContext.Reviews.AddAsync(review);
        }

        public async Task UpdateAsync(Review review)
        {
            var reviewToUpdate = await _dbContext.Reviews.FindAsync(review.Id) 
                ?? throw new Exception("Review not found");
            reviewToUpdate.Rating = review.Rating;
            reviewToUpdate.Comment = review.Comment;
            reviewToUpdate.IsVerifiedPurchase = review.IsVerifiedPurchase;
            reviewToUpdate.IsApproved = review.IsApproved;
            reviewToUpdate.UpdatedAt = DateTime.UtcNow;
        }

        public async Task DeleteAsync(Review review)
        {
            var reviewToDelete = await _dbContext.Reviews.FindAsync(review.Id) 
                ?? throw new Exception("Review not found");
            _dbContext.Reviews.Remove(reviewToDelete);
        }
    }
}
