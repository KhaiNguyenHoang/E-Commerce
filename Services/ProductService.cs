using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repositories;

namespace E_Commerce.Services
{
    public class ProductService(
        IProductRepository productRepository,
        IProductVariantRepository variantRepository,
        IProductImageRepository imageRepository,
        ApplicationDbContext dbContext) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IProductVariantRepository _variantRepository = variantRepository;
        private readonly IProductImageRepository _imageRepository = imageRepository;
        private readonly ApplicationDbContext _dbContext = dbContext;

        // Public
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetActiveAsync()
        {
            return await _productRepository.GetActiveAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        {
            return await _productRepository.GetByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<Product>> GetFeaturedAsync()
        {
            return await _productRepository.GetFeaturedAsync();
        }

        public async Task<IEnumerable<Product>> SearchAsync(string searchTerm)
        {
            return await _productRepository.SearchAsync(searchTerm);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        // Staff+
        public async Task<Product> CreateAsync(Product product)
        {
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;
            
            await _productRepository.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id)
                ?? throw new Exception("Product not found");

            await _productRepository.DeleteAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        // Variants
        public async Task<ProductVariant> AddVariantAsync(int productId, ProductVariant variant)
        {
            // Create new variant to ensure Id is not set (identity column)
            var newVariant = new ProductVariant
            {
                ProductId = productId,
                Size = variant.Size,
                Color = variant.Color,
                ColorCode = variant.ColorCode,
                VariantSKU = variant.VariantSKU,
                StockQuantity = variant.StockQuantity,
                IsAvailable = true, // Default to available
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _variantRepository.AddAsync(newVariant);
            await _dbContext.SaveChangesAsync();

            return newVariant;
        }

        public async Task UpdateVariantAsync(ProductVariant variant)
        {
            await _variantRepository.UpdateAsync(variant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteVariantAsync(int variantId)
        {
            var variant = await _variantRepository.GetByIdAsync(variantId)
                ?? throw new Exception("Variant not found");

            await _variantRepository.DeleteAsync(variant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductVariant>> GetVariantsByProductIdAsync(int productId)
        {
            return await _variantRepository.GetByProductIdAsync(productId);
        }

        // Images
        public async Task<ProductImage> AddImageAsync(int productId, ProductImage image)
        {
            image.ProductId = productId;
            image.CreatedAt = DateTime.UtcNow;
            image.UpdatedAt = DateTime.UtcNow;

            await _imageRepository.AddAsync(image);
            await _dbContext.SaveChangesAsync();

            return image;
        }

        public async Task DeleteImageAsync(int imageId)
        {
            var image = await _imageRepository.GetByIdAsync(imageId)
                ?? throw new Exception("Image not found");

            await _imageRepository.DeleteAsync(image);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductImage>> GetImagesByProductIdAsync(int productId)
        {
            return await _imageRepository.GetByProductIdAsync(productId);
        }
    }
}
