using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services;

public interface IInventoryService
{
    Task<IEnumerable<ProductVariant>> GetLowStockItemsAsync(int threshold = 10);
    Task<bool> CheckStockAsync(int variantId, int quantity);
    Task<bool> ReserveStockAsync(int variantId, int quantity);
    Task ReleaseStockAsync(int variantId, int quantity);
    Task UpdateStockAsync(int variantId, int newQuantity);
    Task<Dictionary<int, int>> GetStockLevelsAsync(IEnumerable<int> variantIds);
}

public class InventoryService : IInventoryService
{
    private readonly ApplicationDbContext _context;

    public InventoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductVariant>> GetLowStockItemsAsync(int threshold = 10)
    {
        return await _context.ProductVariants
            .Where(v => v.StockQuantity <= threshold && v.IsAvailable)
            .Include(v => v.Product)
            .ToListAsync();
    }

    public async Task<bool> CheckStockAsync(int variantId, int quantity)
    {
        var variant = await _context.ProductVariants.FindAsync(variantId);
        return variant != null && variant.StockQuantity >= quantity;
    }

    public async Task<bool> ReserveStockAsync(int variantId, int quantity)
    {
        var variant = await _context.ProductVariants.FindAsync(variantId);
        if (variant == null || variant.StockQuantity < quantity)
            return false;

        variant.StockQuantity -= quantity;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task ReleaseStockAsync(int variantId, int quantity)
    {
        var variant = await _context.ProductVariants.FindAsync(variantId);
        if (variant != null)
        {
            variant.StockQuantity += quantity;
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateStockAsync(int variantId, int newQuantity)
    {
        var variant = await _context.ProductVariants.FindAsync(variantId);
        if (variant != null)
        {
            variant.StockQuantity = newQuantity;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Dictionary<int, int>> GetStockLevelsAsync(IEnumerable<int> variantIds)
    {
        var variants = await _context.ProductVariants
            .Where(v => variantIds.Contains(v.Id))
            .ToListAsync();
        return variants.ToDictionary(v => v.Id, v => v.StockQuantity);
    }
}
