using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repositories;

namespace E_Commerce.Services
{
    public class WishlistService(
        IWishlistRepository wishlistRepository,
        IWishlistItemRepository wishlistItemRepository,
        ICartService cartService,
        ApplicationDbContext dbContext) : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository = wishlistRepository;
        private readonly IWishlistItemRepository _wishlistItemRepository = wishlistItemRepository;
        private readonly ICartService _cartService = cartService;
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Wishlist?> GetWishlistAsync(int userId)
        {
            var wishlist = await _wishlistRepository.GetByUserIdWithItemsAsync(userId);
            
            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await _wishlistRepository.AddAsync(wishlist);
                await _dbContext.SaveChangesAsync();
            }

            return wishlist;
        }

        public async Task<WishlistItem> AddItemAsync(int userId, int productId)
        {
            var wishlist = await GetWishlistAsync(userId)
                ?? throw new Exception("Wishlist not found");

            // Use repository method
            var existing = await _wishlistItemRepository.GetByWishlistIdAndProductIdAsync(wishlist.Id, productId);

            if (existing != null)
            {
                return existing;
            }

            var wishlistItem = new WishlistItem
            {
                WishlistId = wishlist.Id,
                ProductId = productId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _wishlistItemRepository.AddAsync(wishlistItem);
            await _dbContext.SaveChangesAsync();

            return wishlistItem;
        }

        public async Task RemoveItemAsync(int userId, int productId)
        {
            var wishlist = await GetWishlistAsync(userId);
            if (wishlist == null) return;

            // Use repository method
            var item = await _wishlistItemRepository.GetByWishlistIdAndProductIdAsync(wishlist.Id, productId);

            if (item != null)
            {
                await _wishlistItemRepository.DeleteAsync(item);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> IsInWishlistAsync(int userId, int productId)
        {
            var wishlist = await GetWishlistAsync(userId);
            if (wishlist == null) return false;

            // Use repository method
            return await _wishlistItemRepository.ExistsAsync(wishlist.Id, productId);
        }

        public async Task MoveToCartAsync(int userId, int productId, int? variantId, int quantity)
        {
            await _cartService.AddItemAsync(userId, productId, variantId, quantity);
            await RemoveItemAsync(userId, productId);
        }

        public async Task<int> GetWishlistItemCountAsync(int userId)
        {
            var wishlist = await GetWishlistAsync(userId);
            if (wishlist == null) return 0;

            var items = await _wishlistItemRepository.GetByWishlistIdAsync(wishlist.Id);
            return items.Count();
        }
    }
}
