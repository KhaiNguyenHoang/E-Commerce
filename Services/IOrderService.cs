using E_Commerce.Models;

namespace E_Commerce.Services
{
    public interface IOrderService
    {
        // Customer
        Task<Order> CreateOrderAsync(int userId, string shippingName, string shippingPhone, string shippingAddress, string? note, PaymentMethod paymentMethod, string? couponCode = null);
        Task<Order?> GetByIdAsync(int orderId);
        Task<Order?> GetByOrderNumberAsync(string orderNumber);
        Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
        Task CancelOrderAsync(int userId, int orderId);
        
        // Staff+
        Task<IEnumerable<Order>> GetAllAsync();
        Task UpdateStatusAsync(int orderId, OrderStatus status);
        Task UpdatePaymentStatusAsync(int orderId, PaymentStatus status);
        
        // Stats (Admin)
        Task<int> GetTotalOrdersCountAsync();
        Task<decimal> GetTotalRevenueAsync();
    }
}
