using E_Commerce.Data;
using E_Commerce.DTOs.Report;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services;

public class ReportService : IReportService
{
    private readonly ApplicationDbContext _context;

    public ReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardSummaryDto> GetDashboardSummaryAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        var start = startDate ?? DateTime.UtcNow.AddDays(-30);
        var end = endDate ?? DateTime.UtcNow;

        var orders = _context.Orders.AsQueryable();
        var users = _context.Users.AsQueryable();

        var totalOrders = await orders.CountAsync();
        var totalRevenue = await orders
            .Where(o => o.PaymentStatus == PaymentStatus.Paid)
            .SumAsync(o => o.TotalAmount);
        
        var totalUsers = await users.CountAsync();
        var avgOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0;

        return new DashboardSummaryDto
        {
            TotalOrders = totalOrders,
            TotalRevenue = totalRevenue,
            TotalUsers = totalUsers,
            AverageOrderValue = avgOrderValue,
            RevenueStats = await GetRevenueStatsAsync(start, end),
            OrderStatusStats = await GetOrderStatusStatsAsync(),
            TopProducts = await GetTopSellingProductsAsync(5),
            RecentOrders = await _context.Orders
                .Include(o => o.User)
                .OrderByDescending(o => o.CreatedAt)
                .Take(10)
                .ToListAsync()
        };
    }

    public async Task<IEnumerable<RevenueStatDto>> GetRevenueStatsAsync(DateTime startDate, DateTime endDate)
    {
        var data = await _context.Orders
            .Where(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate && o.PaymentStatus == PaymentStatus.Paid)
            .GroupBy(o => o.CreatedAt.Date)
            .Select(g => new
            {
                Date = g.Key,
                Revenue = g.Sum(o => o.TotalAmount),
                Count = g.Count()
            })
            .OrderBy(x => x.Date)
            .ToListAsync();

        return data.Select(x => new RevenueStatDto
        {
            Date = x.Date.ToString("yyyy-MM-dd"),
            Revenue = x.Revenue,
            OrderCount = x.Count
        });
    }

    public async Task<IEnumerable<OrderStatusStatDto>> GetOrderStatusStatsAsync()
    {
        var data = await _context.Orders
            .GroupBy(o => o.Status)
            .Select(g => new
            {
                Status = g.Key,
                Count = g.Count()
            })
            .ToListAsync();

        return data.Select(x => new OrderStatusStatDto
        {
            Status = x.Status.ToString(),
            Count = x.Count
        });
    }

    public async Task<IEnumerable<ProductStatDto>> GetTopSellingProductsAsync(int count = 5)
    {
        var data = await _context.OrderItems
            .Include(oi => oi.Order)
            .Where(oi => oi.Order!.PaymentStatus == PaymentStatus.Paid)
            .GroupBy(oi => new { oi.ProductId, oi.ProductName })
            .Select(g => new
            {
                ProductId = g.Key.ProductId,
                ProductName = g.Key.ProductName,
                Quantity = g.Sum(oi => oi.Quantity),
                Revenue = g.Sum(oi => oi.TotalPrice)
            })
            .OrderByDescending(x => x.Quantity)
            .Take(count)
            .ToListAsync();

        return data.Select(x => new ProductStatDto
        {
            ProductId = x.ProductId,
            ProductName = x.ProductName,
            QuantitySold = x.Quantity,
            TotalRevenue = x.Revenue
        });
    }
}
