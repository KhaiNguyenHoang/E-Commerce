using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services
{
    public class OrderService(
        IOrderRepository orderRepository,
        ICartService cartService,
        ICouponService couponService,
        ApplicationDbContext dbContext) : IOrderService
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly ICartService _cartService = cartService;
        private readonly ICouponService _couponService = couponService;
        private readonly ApplicationDbContext _dbContext = dbContext;

        // Customer
        public async Task<Order> CreateOrderAsync(int userId, string shippingName, string shippingPhone, string shippingAddress, string? note, PaymentMethod paymentMethod, string? couponCode = null)
        {
            var cart = await _cartService.GetCartAsync(userId)
                ?? throw new Exception("Cart not found");

            var cartItems = await _dbContext.CartItems
                .Include(ci => ci.Product)
                .Include(ci => ci.ProductVariant)
                .Where(ci => ci.CartId == cart.Id)
                .ToListAsync();

            if (!cartItems.Any())
            {
                throw new Exception("Cart is empty");
            }

            var subTotal = cartItems.Sum(ci => ci.UnitPrice * ci.Quantity);
            decimal discount = 0;
            int? couponId = null;

            // Apply coupon if provided
            if (!string.IsNullOrEmpty(couponCode))
            {
                if (await _couponService.ValidateCouponAsync(couponCode, subTotal))
                {
                    discount = await _couponService.CalculateDiscountAsync(couponCode, subTotal);
                    var coupon = await _couponService.GetByCodeAsync(couponCode);
                    couponId = coupon?.Id;
                    
                    // Increment usage count
                    if (coupon != null)
                    {
                        await _couponService.IncrementUsageAsync(coupon.Id);
                    }
                }
            }

            var order = new Order
            {
                UserId = userId,
                OrderNumber = GenerateOrderNumber(),
                Status = OrderStatus.Pending,
                PaymentStatus = PaymentStatus.Pending,
                PaymentMethod = paymentMethod,
                ShippingName = shippingName,
                ShippingPhone = shippingPhone,
                ShippingAddress = shippingAddress,
                Note = note,
                CouponId = couponId,
                SubTotal = subTotal,
                DiscountAmount = discount,
                ShippingFee = 0, // Calculate shipping fee as needed
                TotalAmount = subTotal - discount,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Create order items
            order.OrderItems = cartItems.Select(ci => new OrderItem
            {
                ProductId = ci.ProductId,
                ProductVariantId = ci.ProductVariantId,
                ProductName = ci.Product?.Name ?? "Unknown",
                Quantity = ci.Quantity,
                UnitPrice = ci.UnitPrice,
                TotalPrice = ci.UnitPrice * ci.Quantity,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }).ToList();

            await _orderRepository.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            // Clear cart after order (but not for Stripe - will clear after payment success)
            if (paymentMethod != PaymentMethod.Stripe)
            {
                await _cartService.ClearCartAsync(userId);
            }

            return order;
        }

        public async Task<Order?> GetByIdAsync(int orderId)
        {
            return await _orderRepository.GetByIdAsync(orderId);
        }

        public async Task<Order?> GetByOrderNumberAsync(string orderNumber)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
        {
            return await _orderRepository.GetByUserIdAsync(userId);
        }

        public async Task CancelOrderAsync(int userId, int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId)
                ?? throw new Exception("Order not found");

            if (order.UserId != userId)
            {
                throw new Exception("Unauthorized");
            }

            if (order.Status != OrderStatus.Pending)
            {
                throw new Exception("Only pending orders can be cancelled");
            }

            order.Status = OrderStatus.Cancelled;
            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        // Staff+
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task UpdateStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _orderRepository.GetByIdAsync(orderId)
                ?? throw new Exception("Order not found");

            order.Status = status;
            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePaymentStatusAsync(int orderId, PaymentStatus status)
        {
            var order = await _orderRepository.GetByIdAsync(orderId)
                ?? throw new Exception("Order not found");

            order.PaymentStatus = status;
            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        // Admin stats
        public async Task<int> GetTotalOrdersCountAsync()
        {
            return await _dbContext.Orders.CountAsync();
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _dbContext.Orders
                .Where(o => o.Status == OrderStatus.Delivered && o.PaymentStatus == PaymentStatus.Paid)
                .SumAsync(o => o.TotalAmount);
        }

        private static string GenerateOrderNumber()
        {
            return $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{Random.Shared.Next(1000, 9999)}";
        }
    }
}
