using E_Commerce.Models;

namespace E_Commerce.Repositories
{
    public interface IProductVariantRepository
    {
        Task<ProductVariant?> GetByIdAsync(int id);
        Task<IEnumerable<ProductVariant>> GetByProductIdAsync(int productId);
        Task<ProductVariant?> GetByProductIdAndSizeAndColorAsync(int productId, string size, string color);
        Task<IEnumerable<ProductVariant>> GetAvailableByProductIdAsync(int productId);
        Task AddAsync(ProductVariant productVariant);
        Task UpdateAsync(ProductVariant productVariant);
        Task DeleteAsync(ProductVariant productVariant);
    }
}
