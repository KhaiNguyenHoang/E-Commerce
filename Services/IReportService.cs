using E_Commerce.DTOs.Report;
using E_Commerce.Models;

namespace E_Commerce.Services;

public interface IReportService
{
    Task<DashboardSummaryDto> GetDashboardSummaryAsync(DateTime? startDate = null, DateTime? endDate = null);
    Task<IEnumerable<RevenueStatDto>> GetRevenueStatsAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<OrderStatusStatDto>> GetOrderStatusStatsAsync();
    Task<IEnumerable<ProductStatDto>> GetTopSellingProductsAsync(int count = 5);
}
