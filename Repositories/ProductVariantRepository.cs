using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories
{
    public class ProductVariantRepository(ApplicationDbContext dbContext) : IProductVariantRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<ProductVariant?> GetByIdAsync(int id)
        {
            return await _dbContext.ProductVariants
                .Include(pv => pv.Product)
                .FirstOrDefaultAsync(pv => pv.Id == id);
        }

        public async Task<IEnumerable<ProductVariant>> GetByProductIdAsync(int productId)
        {
            return await _dbContext.ProductVariants
                .Where(pv => pv.ProductId == productId)
                .ToListAsync();
        }

        public async Task<ProductVariant?> GetByProductIdAndSizeAndColorAsync(int productId, string size, string color)
        {
            return await _dbContext.ProductVariants
                .FirstOrDefaultAsync(pv => pv.ProductId == productId && pv.Size == size && pv.Color == color);
        }

        public async Task<IEnumerable<ProductVariant>> GetAvailableByProductIdAsync(int productId)
        {
            return await _dbContext.ProductVariants
                .Where(pv => pv.ProductId == productId && pv.IsAvailable && pv.StockQuantity > 0)
                .ToListAsync();
        }

        public async Task AddAsync(ProductVariant productVariant)
        {
            productVariant.CreatedAt = DateTime.UtcNow;
            productVariant.UpdatedAt = DateTime.UtcNow;
            await _dbContext.ProductVariants.AddAsync(productVariant);
        }

        public async Task UpdateAsync(ProductVariant productVariant)
        {
            var variantToUpdate = await _dbContext.ProductVariants.FindAsync(productVariant.Id) 
                ?? throw new Exception("Product variant not found");
            variantToUpdate.Size = productVariant.Size;
            variantToUpdate.Color = productVariant.Color;
            variantToUpdate.ColorCode = productVariant.ColorCode;
            variantToUpdate.StockQuantity = productVariant.StockQuantity;
            variantToUpdate.VariantSKU = productVariant.VariantSKU;
            variantToUpdate.IsAvailable = productVariant.IsAvailable;
            variantToUpdate.UpdatedAt = DateTime.UtcNow;
        }

        public async Task DeleteAsync(ProductVariant productVariant)
        {
            var variantToDelete = await _dbContext.ProductVariants.FindAsync(productVariant.Id) 
                ?? throw new Exception("Product variant not found");
            _dbContext.ProductVariants.Remove(variantToDelete);
        }
    }
}
