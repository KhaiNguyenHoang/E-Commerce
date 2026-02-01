using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers;

public class PaymentController : BaseController
{
    private readonly IPaymentService _paymentService;
    private readonly IOrderService _orderService;

    public PaymentController(IPaymentService paymentService, IOrderService orderService)
    {
        _paymentService = paymentService;
        _orderService = orderService;
    }

    // GET: /Payment/VnPay/5
    [RequireAuth]
    public async Task<IActionResult> VnPay(int orderId)
    {
        var order = await _orderService.GetByIdAsync(orderId);
        if (order == null) return NotFound();

        var userId = await GetCurrentUserIdAsync();
        if (order.UserId != userId) return Forbid();

        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var request = new PaymentRequestDto
        {
            OrderId = order.Id,
            OrderNumber = order.OrderNumber,
            Amount = order.TotalAmount,
            Description = $"Payment for order {order.OrderNumber}",
            ReturnUrl = $"{baseUrl}/Payment/VnPayCallback"
        };

        var result = await _paymentService.CreateVnPayPaymentAsync(request);
        
        if (result.Success && !string.IsNullOrEmpty(result.PaymentUrl))
            return Redirect(result.PaymentUrl);

        TempData["Error"] = result.Message ?? "Payment failed";
        return RedirectToAction("Details", "Order", new { id = orderId });
    }

    // GET: /Payment/VnPayCallback
    public async Task<IActionResult> VnPayCallback()
    {
        var result = await _paymentService.ProcessVnPayCallbackAsync(Request.Query);
        
        var orderNumber = Request.Query["vnp_TxnRef"].ToString();
        var order = await _orderService.GetByOrderNumberAsync(orderNumber);

        if (result.Success)
        {
            TempData["Success"] = "Payment successful!";
        }
        else
        {
            TempData["Error"] = result.Message ?? "Payment failed";
        }

        return RedirectToAction("Details", "Order", new { id = order?.Id });
    }

    // GET: /Payment/Momo/5
    [RequireAuth]
    public async Task<IActionResult> Momo(int orderId)
    {
        var order = await _orderService.GetByIdAsync(orderId);
        if (order == null) return NotFound();

        var userId = await GetCurrentUserIdAsync();
        if (order.UserId != userId) return Forbid();

        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var request = new PaymentRequestDto
        {
            OrderId = order.Id,
            OrderNumber = order.OrderNumber,
            Amount = order.TotalAmount,
            Description = $"Payment for order {order.OrderNumber}",
            ReturnUrl = $"{baseUrl}/Payment/MomoCallback"
        };

        var result = await _paymentService.CreateMomoPaymentAsync(request);
        
        if (result.Success && !string.IsNullOrEmpty(result.PaymentUrl))
            return Redirect(result.PaymentUrl);

        TempData["Error"] = result.Message ?? "Payment failed";
        return RedirectToAction("Details", "Order", new { id = orderId });
    }

    // Payment Status Page
    [RequireAuth]
    public async Task<IActionResult> Status(int orderId)
    {
        var order = await _orderService.GetByIdAsync(orderId);
        if (order == null) return NotFound();
        return View(order);
    }
}
