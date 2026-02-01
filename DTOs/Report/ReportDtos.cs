using E_Commerce.Models;

namespace E_Commerce.DTOs.Report;

public class RevenueStatDto
{
    public string Date { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
    public int OrderCount { get; set; }
}

public class OrderStatusStatDto
{
    public string Status { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class ProductStatDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int QuantitySold { get; set; }
    public decimal TotalRevenue { get; set; }
}

public class DashboardSummaryDto
{
    public decimal TotalRevenue { get; set; }
    public int TotalOrders { get; set; }
    public int TotalUsers { get; set; }
    public decimal AverageOrderValue { get; set; }
    
    public IEnumerable<RevenueStatDto> RevenueStats { get; set; } = [];
    public IEnumerable<OrderStatusStatDto> OrderStatusStats { get; set; } = [];
    public IEnumerable<ProductStatDto> TopProducts { get; set; } = [];
    public IEnumerable<E_Commerce.Models.Order> RecentOrders { get; set; } = [];
}
