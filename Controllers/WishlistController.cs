using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [RequireAuth]
    public class WishlistController(IWishlistService wishlistService) : BaseController
    {
        private readonly IWishlistService _wishlistService = wishlistService;

        // GET: /Wishlist
        public async Task<IActionResult> Index()
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            var wishlist = await _wishlistService.GetWishlistAsync(userId.Value);
            return View(wishlist);
        }

        // GET/POST: /Wishlist/Add/5 or /Wishlist/Add?productId=5
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Add(int? id, int? productId, string? returnUrl = null)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            // Support both /Wishlist/Add/5 (id) and /Wishlist/Add?productId=5
            var prodId = productId ?? id;
            if (prodId == null)
            {
                TempData["Error"] = "Product not specified";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _wishlistService.AddItemAsync(userId.Value, prodId.Value);
                TempData["Success"] = "Item added to wishlist";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Wishlist/Remove
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int productId)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            try
            {
                await _wishlistService.RemoveItemAsync(userId.Value, productId);
                TempData["Success"] = "Item removed from wishlist";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Wishlist/MoveToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MoveToCart(int productId, int? variantId, int quantity = 1)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            try
            {
                await _wishlistService.MoveToCartAsync(userId.Value, productId, variantId, quantity);
                TempData["Success"] = "Item moved to cart";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Wishlist/Count (for AJAX)
        [HttpGet]
        public async Task<IActionResult> Count()
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return Json(0);

            var count = await _wishlistService.GetWishlistItemCountAsync(userId.Value);
            return Json(count);
        }
    }
}
