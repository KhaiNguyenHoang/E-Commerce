using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories
{
    public class OrderRepository(ApplicationDbContext context) : IOrderRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddAsync(Order order)
        {
            order.CreatedAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;
            await _context.Orders.AddAsync(order);
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id)
                ?? throw new Exception("Order not found");
            _context.Orders.Remove(order);
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariant)
                .FirstOrDefaultAsync(o => o.Id == id)
                ?? throw new Exception("Order not found");
        }

        public async Task<Order> GetByNumberAsync(string number)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderNumber == number)
                ?? throw new Exception("Order not found");
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByUserIdAndProductIdAsync(int userId, int productId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId && o.OrderItems.Any(oi => oi.ProductId == productId))
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByUserIdAndProductIdAndProductVariantIdAsync(int userId, int productId, int productVariantId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariant)
                .Where(o => o.UserId == userId &&
                    o.OrderItems.Any(oi => oi.ProductId == productId && oi.ProductVariantId == productVariantId))
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            var orderToUpdate = await _context.Orders.FindAsync(order.Id)
                ?? throw new Exception("Order not found");

            orderToUpdate.Status = order.Status;
            orderToUpdate.PaymentStatus = order.PaymentStatus;
            orderToUpdate.ShippingAddress = order.ShippingAddress;
            orderToUpdate.ShippingPhone = order.ShippingPhone;
            orderToUpdate.ShippingName = order.ShippingName;
            orderToUpdate.Note = order.Note;
            orderToUpdate.UpdatedAt = DateTime.UtcNow;
        }
    }
}
