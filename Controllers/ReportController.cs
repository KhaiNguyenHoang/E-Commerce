using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers;

public class ReportController : Controller
{
    private readonly IReportService _reportService;
    private readonly IAuthService _authService;

    public ReportController(IReportService reportService, IAuthService authService)
    {
        _reportService = reportService;
        _authService = authService;
    }

    public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
    {
        if (!await _authService.IsAdminAsync())
        {
            return RedirectToAction("Index", "Home");
        }

        var data = await _reportService.GetDashboardSummaryAsync(startDate, endDate);
        return View(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetRevenueData(DateTime? startDate, DateTime? endDate)
    {
        if (!await _authService.IsAdminAsync())
        {
            return Unauthorized();
        }

        var start = startDate ?? DateTime.UtcNow.AddDays(-30);
        var end = endDate ?? DateTime.UtcNow;

        var stats = await _reportService.GetRevenueStatsAsync(start, end);
        return Json(stats);
    }
}
