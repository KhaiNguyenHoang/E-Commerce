using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories
{
    public class ProductRepository(ApplicationDbContext dbContext) : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.ProductVariants)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> GetBySkuAsync(string sku)
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.SKU == sku);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId && p.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetFeaturedAsync()
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .Where(p => p.IsFeatured && p.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetActiveAsync()
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchAsync(string searchTerm)
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive && 
                    (p.Name.Contains(searchTerm) || 
                     p.Description!.Contains(searchTerm) || 
                     p.Brand.Contains(searchTerm)))
                .ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;
            await _dbContext.Products.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            var productToUpdate = await _dbContext.Products.FindAsync(product.Id) 
                ?? throw new Exception("Product not found");
            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;
            productToUpdate.DiscountPrice = product.DiscountPrice;
            productToUpdate.Brand = product.Brand;
            productToUpdate.SKU = product.SKU;
            productToUpdate.MainImageUrl = product.MainImageUrl;
            productToUpdate.IsActive = product.IsActive;
            productToUpdate.IsFeatured = product.IsFeatured;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UpdatedAt = DateTime.UtcNow;
        }

        public async Task DeleteAsync(Product product)
        {
            var productToDelete = await _dbContext.Products.FindAsync(product.Id) 
                ?? throw new Exception("Product not found");
            productToDelete.IsActive = false;
            productToDelete.UpdatedAt = DateTime.UtcNow;
        }
    }
}
