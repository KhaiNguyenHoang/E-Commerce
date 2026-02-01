using E_Commerce.Models;

namespace E_Commerce.Services
{
    public interface IProductService
    {
        // Public
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetActiveAsync();
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> GetFeaturedAsync();
        Task<IEnumerable<Product>> SearchAsync(string searchTerm);
        Task<Product?> GetByIdAsync(int id);
        
        // Staff+
        Task<Product> CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        
        // Variants (Staff+)
        Task<ProductVariant> AddVariantAsync(int productId, ProductVariant variant);
        Task UpdateVariantAsync(ProductVariant variant);
        Task DeleteVariantAsync(int variantId);
        Task<IEnumerable<ProductVariant>> GetVariantsByProductIdAsync(int productId);
        
        // Images (Staff+)
        Task<ProductImage> AddImageAsync(int productId, ProductImage image);
        Task DeleteImageAsync(int imageId);
        Task<IEnumerable<ProductImage>> GetImagesByProductIdAsync(int productId);
    }
}
