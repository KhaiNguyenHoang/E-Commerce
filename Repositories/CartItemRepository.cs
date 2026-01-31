using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories
{
    public class CartItemRepository(ApplicationDbContext dbContext) : ICartItemRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<CartItem?> GetByIdAsync(int id)
        {
            return await _dbContext.CartItems
                .Include(ci => ci.Product)
                .Include(ci => ci.ProductVariant)
                .FirstOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task<IEnumerable<CartItem>> GetByCartIdAsync(int cartId)
        {
            return await _dbContext.CartItems
                .Include(ci => ci.Product)
                .Include(ci => ci.ProductVariant)
                .Where(ci => ci.CartId == cartId)
                .ToListAsync();
        }

        public async Task<CartItem?> GetByCartIdAndProductIdAsync(int cartId, int productId, int? productVariantId)
        {
            return await _dbContext.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartId &&
                    ci.ProductId == productId &&
                    ci.ProductVariantId == productVariantId);
        }

        public async Task AddAsync(CartItem cartItem)
        {
            cartItem.CreatedAt = DateTime.UtcNow;
            cartItem.UpdatedAt = DateTime.UtcNow;
            await _dbContext.CartItems.AddAsync(cartItem);
        }

        public async Task UpdateAsync(CartItem cartItem)
        {
            var itemToUpdate = await _dbContext.CartItems.FindAsync(cartItem.Id)
                ?? throw new Exception("Cart item not found");
            itemToUpdate.Quantity = cartItem.Quantity;
            itemToUpdate.SelectedSize = cartItem.SelectedSize;
            itemToUpdate.SelectedColor = cartItem.SelectedColor;
            itemToUpdate.UnitPrice = cartItem.UnitPrice;
            itemToUpdate.ProductVariantId = cartItem.ProductVariantId;
            itemToUpdate.UpdatedAt = DateTime.UtcNow;
        }

        public async Task DeleteAsync(CartItem cartItem)
        {
            var itemToDelete = await _dbContext.CartItems.FindAsync(cartItem.Id)
                ?? throw new Exception("Cart item not found");
            _dbContext.CartItems.Remove(itemToDelete);
        }

        public async Task ClearCartAsync(int cartId)
        {
            var itemsToDelete = await _dbContext.CartItems
                .Where(ci => ci.CartId == cartId)
                .ToListAsync();
            _dbContext.CartItems.RemoveRange(itemsToDelete);
        }
    }
}
