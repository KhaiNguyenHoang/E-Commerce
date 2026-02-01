using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [RequireAuth]
    public class CartController(ICartService cartService) : BaseController
    {
        private readonly ICartService _cartService = cartService;

        // GET: /Cart
        public async Task<IActionResult> Index()
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            var cart = await _cartService.GetCartAsync(userId.Value);
            ViewBag.Total = await _cartService.GetCartTotalAsync(userId.Value);
            return View(cart);
        }

        // POST: /Cart/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int productId, int? variantId, int quantity = 1)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            try
            {
                await _cartService.AddItemAsync(userId.Value, productId, variantId, quantity);
                TempData["Success"] = "Item added to cart";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Cart/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int cartItemId, int quantity)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            try
            {
                await _cartService.UpdateItemQuantityAsync(userId.Value, cartItemId, quantity);
                TempData["Success"] = "Cart updated";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Cart/Remove
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int cartItemId)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            try
            {
                await _cartService.RemoveItemAsync(userId.Value, cartItemId);
                TempData["Success"] = "Item removed from cart";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Cart/Clear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Clear()
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            await _cartService.ClearCartAsync(userId.Value);
            TempData["Success"] = "Cart cleared";

            return RedirectToAction(nameof(Index));
        }

        // GET: /Cart/Count (for AJAX)
        [HttpGet]
        public async Task<IActionResult> Count()
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return Json(0);

            var count = await _cartService.GetCartItemCountAsync(userId.Value);
            return Json(count);
        }
    }
}
