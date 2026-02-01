using E_Commerce.Models;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class OrderController(
        IOrderService orderService,
        IAddressService addressService,
        ICartService cartService,
        ICouponService couponService,
        IStripeService stripeService) : BaseController
    {
        private readonly IOrderService _orderService = orderService;
        private readonly IAddressService _addressService = addressService;
        private readonly ICartService _cartService = cartService;
        private readonly ICouponService _couponService = couponService;
        private readonly IStripeService _stripeService = stripeService;

        // GET: /Order (Customer - their orders)
        [RequireAuth]
        public async Task<IActionResult> Index()
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            var orders = await _orderService.GetByUserIdAsync(userId.Value);
            return View(orders);
        }

        // GET: /Order/Details/5
        [RequireAuth]
        public async Task<IActionResult> Details(int id)
        {
            var userId = await GetCurrentUserIdAsync();
            var order = await _orderService.GetByIdAsync(id);

            if (order == null) return NotFound();

            // Only owner or staff can view
            var isStaffOrAdmin = await IsStaffOrAdminAsync();
            if (order.UserId != userId && !isStaffOrAdmin)
            {
                return Forbid();
            }

            ViewBag.IsStaffOrAdmin = isStaffOrAdmin;
            return View(order);
        }

        // GET: /Order/Checkout
        [RequireAuth]
        public async Task<IActionResult> Checkout()
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            var cart = await _cartService.GetCartAsync(userId.Value);
            var cartItemCount = await _cartService.GetCartItemCountAsync(userId.Value);

            if (cartItemCount == 0)
            {
                TempData["Error"] = "Your cart is empty";
                return RedirectToAction("Index", "Cart");
            }

            var total = await _cartService.GetCartTotalAsync(userId.Value);

            // Check for applied coupon in session
            var couponCode = HttpContext.Session.GetString("AppliedCoupon");
            Coupon? appliedCoupon = null;
            decimal discount = 0;

            if (!string.IsNullOrEmpty(couponCode))
            {
                appliedCoupon = await _couponService.GetByCodeAsync(couponCode);
                if (appliedCoupon != null && await _couponService.ValidateCouponAsync(couponCode, total))
                {
                    discount = await _couponService.CalculateDiscountAsync(couponCode, total);
                }
                else
                {
                    HttpContext.Session.Remove("AppliedCoupon");
                    appliedCoupon = null;
                }
            }

            ViewBag.Cart = cart;
            ViewBag.Total = total;
            ViewBag.Addresses = await _addressService.GetByUserIdAsync(userId.Value);
            ViewBag.AppliedCoupon = appliedCoupon;
            ViewBag.Discount = discount;
            return View();
        }

        // GET: /Order/ApplyCoupon
        [RequireAuth]
        public async Task<IActionResult> ApplyCoupon(string code)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            var total = await _cartService.GetCartTotalAsync(userId.Value);

            try
            {
                if (await _couponService.ValidateCouponAsync(code, total))
                {
                    HttpContext.Session.SetString("AppliedCoupon", code.ToUpper());
                    TempData["Success"] = "Coupon applied successfully!";
                }
                else
                {
                    TempData["Error"] = "Invalid or expired coupon code";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Checkout));
        }

        // GET: /Order/RemoveCoupon
        [RequireAuth]
        public IActionResult RemoveCoupon()
        {
            HttpContext.Session.Remove("AppliedCoupon");
            TempData["Success"] = "Coupon removed";
            return RedirectToAction(nameof(Checkout));
        }

        // POST: /Order/PlaceOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireAuth]
        public async Task<IActionResult> PlaceOrder(string shippingName, string shippingPhone, string shippingAddress, string? note, PaymentMethod paymentMethod, string? couponCode)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            try
            {
                var order = await _orderService.CreateOrderAsync(
                    userId.Value,
                    shippingName,
                    shippingPhone,
                    shippingAddress,
                    note,
                    paymentMethod,
                    couponCode);

                // Clear applied coupon from session
                HttpContext.Session.Remove("AppliedCoupon");

                // Handle Stripe payment
                if (paymentMethod == PaymentMethod.Stripe)
                {
                    var baseUrl = $"{Request.Scheme}://{Request.Host}";
                    var successUrl = $"{baseUrl}/Order/PaymentSuccess/{order.Id}";
                    var cancelUrl = $"{baseUrl}/Order/PaymentCancel/{order.Id}";

                    var checkoutUrl = await _stripeService.CreateCheckoutSessionAsync(order.Id, successUrl, cancelUrl);
                    return Redirect(checkoutUrl);
                }

                TempData["Success"] = $"Order {order.OrderNumber} placed successfully!";
                return RedirectToAction(nameof(Details), new { id = order.Id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Checkout));
            }
        }

        // POST: /Order/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireAuth]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            try
            {
                await _orderService.CancelOrderAsync(userId.Value, id);
                TempData["Success"] = "Order cancelled";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /Order/Manage (Staff+)
        [RequireRole("Staff", "Admin")]
        public async Task<IActionResult> Manage()
        {
            var orders = await _orderService.GetAllAsync();
            return View(orders);
        }

        // POST: /Order/UpdateStatus (Staff+)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireRole("Staff", "Admin")]
        public async Task<IActionResult> UpdateStatus(int id, OrderStatus status)
        {
            try
            {
                await _orderService.UpdateStatusAsync(id, status);
                TempData["Success"] = "Order status updated";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        // POST: /Order/UpdatePaymentStatus (Staff+)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireRole("Staff", "Admin")]
        public async Task<IActionResult> UpdatePaymentStatus(int id, PaymentStatus status)
        {
            try
            {
                await _orderService.UpdatePaymentStatusAsync(id, status);
                TempData["Success"] = "Payment status updated";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /Order/PaymentSuccess/5
        [RequireAuth]
        public async Task<IActionResult> PaymentSuccess(int id)
        {
            var userId = await GetCurrentUserIdAsync();
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();

            // Update payment status on success callback
            await _orderService.UpdatePaymentStatusAsync(id, PaymentStatus.Paid);
            await _orderService.UpdateStatusAsync(id, OrderStatus.Confirmed);

            // Clear cart after successful Stripe payment
            if (userId != null)
            {
                await _cartService.ClearCartAsync(userId.Value);
            }

            TempData["Success"] = "Payment successful! Your order has been confirmed.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /Order/PaymentCancel/5
        [RequireAuth]
        public async Task<IActionResult> PaymentCancel(int id)
        {
            TempData["Error"] = "Payment was cancelled. You can try again or choose a different payment method.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // POST: /Order/StripeWebhook
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var signature = Request.Headers["Stripe-Signature"].ToString();

            var success = await _stripeService.HandleWebhookAsync(json, signature);
            return success ? Ok() : BadRequest();
        }
    }
}

