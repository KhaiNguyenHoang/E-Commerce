using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repositories;

namespace E_Commerce.Services
{
    public class CartService(
        ICartRepository cartRepository,
        ICartItemRepository cartItemRepository,
        IProductRepository productRepository,
        ApplicationDbContext dbContext) : ICartService
    {
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly ICartItemRepository _cartItemRepository = cartItemRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Cart?> GetCartAsync(int userId)
        {
            var cart = await _cartRepository.GetByUserIdAsync(userId);
            
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await _cartRepository.AddAsync(cart);
                await _dbContext.SaveChangesAsync();
            }

            return cart;
        }

        public async Task<CartItem> AddItemAsync(int userId, int productId, int? variantId, int quantity)
        {
            var cart = await GetCartAsync(userId) 
                ?? throw new Exception("Cart not found");

            var product = await _productRepository.GetByIdAsync(productId) 
                ?? throw new Exception("Product not found");

            // Use repository method to find existing item
            var existingItem = await _cartItemRepository.GetByCartIdAndProductIdAsync(cart.Id, productId, variantId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.UpdatedAt = DateTime.UtcNow;
                await _cartItemRepository.UpdateAsync(existingItem);
                await _dbContext.SaveChangesAsync();
                return existingItem;
            }

            var cartItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = productId,
                ProductVariantId = variantId,
                Quantity = quantity,
                UnitPrice = product.Price,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _cartItemRepository.AddAsync(cartItem);
            await _dbContext.SaveChangesAsync();

            return cartItem;
        }

        public async Task UpdateItemQuantityAsync(int userId, int cartItemId, int quantity)
        {
            var cart = await GetCartAsync(userId) 
                ?? throw new Exception("Cart not found");

            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId)
                ?? throw new Exception("Cart item not found");

            if (cartItem.CartId != cart.Id)
            {
                throw new Exception("Unauthorized");
            }

            if (quantity <= 0)
            {
                await _cartItemRepository.DeleteAsync(cartItem);
            }
            else
            {
                cartItem.Quantity = quantity;
                cartItem.UpdatedAt = DateTime.UtcNow;
                await _cartItemRepository.UpdateAsync(cartItem);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(int userId, int cartItemId)
        {
            var cart = await GetCartAsync(userId) 
                ?? throw new Exception("Cart not found");

            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId)
                ?? throw new Exception("Cart item not found");

            if (cartItem.CartId != cart.Id)
            {
                throw new Exception("Unauthorized");
            }

            await _cartItemRepository.DeleteAsync(cartItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await GetCartAsync(userId);
            if (cart == null) return;

            // Use repository method
            await _cartItemRepository.ClearCartAsync(cart.Id);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<decimal> GetCartTotalAsync(int userId)
        {
            var cart = await GetCartAsync(userId);
            if (cart == null) return 0;

            var items = await _cartItemRepository.GetByCartIdAsync(cart.Id);
            return items.Sum(i => i.UnitPrice * i.Quantity);
        }

        public async Task<int> GetCartItemCountAsync(int userId)
        {
            var cart = await GetCartAsync(userId);
            if (cart == null) return 0;

            var items = await _cartItemRepository.GetByCartIdAsync(cart.Id);
            return items.Sum(i => i.Quantity);
        }
    }
}
