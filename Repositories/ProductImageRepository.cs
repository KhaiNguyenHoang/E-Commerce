using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories
{
    public class ProductImageRepository(ApplicationDbContext dbContext) : IProductImageRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<ProductImage?> GetByIdAsync(int id)
        {
            return await _dbContext.ProductImages.FindAsync(id);
        }

        public async Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId)
        {
            return await _dbContext.ProductImages
                .Where(pi => pi.ProductId == productId)
                .OrderBy(pi => pi.DisplayOrder)
                .ToListAsync();
        }

        public async Task<ProductImage?> GetMainImageByProductIdAsync(int productId)
        {
            return await _dbContext.ProductImages
                .FirstOrDefaultAsync(pi => pi.ProductId == productId && pi.IsMain);
        }

        public async Task AddAsync(ProductImage productImage)
        {
            productImage.CreatedAt = DateTime.UtcNow;
            productImage.UpdatedAt = DateTime.UtcNow;
            await _dbContext.ProductImages.AddAsync(productImage);
        }

        public async Task UpdateAsync(ProductImage productImage)
        {
            var imageToUpdate = await _dbContext.ProductImages.FindAsync(productImage.Id) 
                ?? throw new Exception("Product image not found");
            imageToUpdate.ImageUrl = productImage.ImageUrl;
            imageToUpdate.AltText = productImage.AltText;
            imageToUpdate.DisplayOrder = productImage.DisplayOrder;
            imageToUpdate.IsMain = productImage.IsMain;
            imageToUpdate.UpdatedAt = DateTime.UtcNow;
        }

        public async Task DeleteAsync(ProductImage productImage)
        {
            var imageToDelete = await _dbContext.ProductImages.FindAsync(productImage.Id) 
                ?? throw new Exception("Product image not found");
            _dbContext.ProductImages.Remove(imageToDelete);
        }
    }
}
