using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace E_Commerce.Services;

public class PaymentRequestDto
{
    public int OrderId { get; set; }
    public string OrderNumber { get; set; } = "";
    public decimal Amount { get; set; }
    public string Description { get; set; } = "";
    public string ReturnUrl { get; set; } = "";
}

public class PaymentResultDto
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? TransactionId { get; set; }
    public string? PaymentUrl { get; set; }
}

public interface IPaymentService
{
    Task<PaymentResultDto> CreateVnPayPaymentAsync(PaymentRequestDto request);
    Task<PaymentResultDto> ProcessVnPayCallbackAsync(IQueryCollection query);
    Task<PaymentResultDto> CreateMomoPaymentAsync(PaymentRequestDto request);
    Task<PaymentResultDto> ProcessMomoCallbackAsync(string data);
}

public class PaymentService : IPaymentService
{
    private readonly IConfiguration _config;
    private readonly ILogger<PaymentService> _logger;
    private readonly IOrderService _orderService;

    public PaymentService(IConfiguration config, ILogger<PaymentService> logger, IOrderService orderService)
    {
        _config = config;
        _logger = logger;
        _orderService = orderService;
    }

    // VNPay Integration
    public async Task<PaymentResultDto> CreateVnPayPaymentAsync(PaymentRequestDto request)
    {
        var vnpUrl = _config["VnPay:Url"] ?? "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        var vnpTmnCode = _config["VnPay:TmnCode"];
        var vnpHashSecret = _config["VnPay:HashSecret"];

        if (string.IsNullOrEmpty(vnpTmnCode) || string.IsNullOrEmpty(vnpHashSecret))
        {
            return new PaymentResultDto { Success = false, Message = "VNPay not configured" };
        }

        var vnpParams = new SortedDictionary<string, string>
        {
            { "vnp_Version", "2.1.0" },
            { "vnp_Command", "pay" },
            { "vnp_TmnCode", vnpTmnCode },
            { "vnp_Amount", ((int)(request.Amount * 100)).ToString() },
            { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") },
            { "vnp_CurrCode", "VND" },
            { "vnp_IpAddr", "127.0.0.1" },
            { "vnp_Locale", "vn" },
            { "vnp_OrderInfo", request.Description },
            { "vnp_OrderType", "other" },
            { "vnp_ReturnUrl", request.ReturnUrl },
            { "vnp_TxnRef", request.OrderNumber }
        };

        var queryString = string.Join("&", vnpParams.Select(kv => $"{kv.Key}={HttpUtility.UrlEncode(kv.Value)}"));
        var signData = string.Join("&", vnpParams.Select(kv => $"{kv.Key}={kv.Value}"));
        var secureHash = HmacSha512(vnpHashSecret, signData);

        var paymentUrl = $"{vnpUrl}?{queryString}&vnp_SecureHash={secureHash}";

        return await Task.FromResult(new PaymentResultDto
        {
            Success = true,
            PaymentUrl = paymentUrl
        });
    }

    public async Task<PaymentResultDto> ProcessVnPayCallbackAsync(IQueryCollection query)
    {
        var vnpHashSecret = _config["VnPay:HashSecret"];
        if (string.IsNullOrEmpty(vnpHashSecret))
            return new PaymentResultDto { Success = false, Message = "VNPay not configured" };

        var vnpSecureHash = query["vnp_SecureHash"].ToString();
        var responseCode = query["vnp_ResponseCode"].ToString();
        var txnRef = query["vnp_TxnRef"].ToString();
        var transactionNo = query["vnp_TransactionNo"].ToString();

        // Verify hash
        var signParams = query
            .Where(kv => kv.Key.StartsWith("vnp_") && kv.Key != "vnp_SecureHash" && kv.Key != "vnp_SecureHashType")
            .OrderBy(kv => kv.Key)
            .ToDictionary(kv => kv.Key, kv => kv.Value.ToString());

        var signData = string.Join("&", signParams.Select(kv => $"{kv.Key}={kv.Value}"));
        var checkSum = HmacSha512(vnpHashSecret, signData);

        if (checkSum != vnpSecureHash)
            return new PaymentResultDto { Success = false, Message = "Invalid signature" };

        if (responseCode == "00")
        {
            // Update order payment status
            var order = await _orderService.GetByOrderNumberAsync(txnRef);
            if (order != null)
            {
                await _orderService.UpdatePaymentStatusAsync(order.Id, E_Commerce.Models.PaymentStatus.Paid);
            }

            return new PaymentResultDto
            {
                Success = true,
                Message = "Payment successful",
                TransactionId = transactionNo
            };
        }

        return new PaymentResultDto { Success = false, Message = $"Payment failed: {responseCode}" };
    }

    // Momo Integration (simplified)
    public async Task<PaymentResultDto> CreateMomoPaymentAsync(PaymentRequestDto request)
    {
        var momoEndpoint = _config["Momo:Endpoint"] ?? "https://test-payment.momo.vn/v2/gateway/api/create";
        var partnerCode = _config["Momo:PartnerCode"];
        var accessKey = _config["Momo:AccessKey"];
        var secretKey = _config["Momo:SecretKey"];

        if (string.IsNullOrEmpty(partnerCode) || string.IsNullOrEmpty(secretKey))
        {
            return await Task.FromResult(new PaymentResultDto { Success = false, Message = "Momo not configured" });
        }

        var requestId = Guid.NewGuid().ToString();
        var orderId = request.OrderNumber;
        var amount = (long)request.Amount;
        var orderInfo = request.Description;
        var redirectUrl = request.ReturnUrl;
        var ipnUrl = request.ReturnUrl.Replace("/callback", "/ipn");
        var requestType = "captureWallet";

        var rawSignature = $"accessKey={accessKey}&amount={amount}&extraData=&ipnUrl={ipnUrl}&orderId={orderId}&orderInfo={orderInfo}&partnerCode={partnerCode}&redirectUrl={redirectUrl}&requestId={requestId}&requestType={requestType}";
        var signature = HmacSha256(secretKey!, rawSignature);

        // In real implementation, send HTTP POST to Momo endpoint
        // For now, return placeholder
        return await Task.FromResult(new PaymentResultDto
        {
            Success = true,
            Message = "Momo payment created (mock)",
            PaymentUrl = $"{momoEndpoint}?orderId={orderId}"
        });
    }

    public async Task<PaymentResultDto> ProcessMomoCallbackAsync(string data)
    {
        // Process Momo callback
        _logger.LogInformation("Momo callback received: {Data}", data);
        return await Task.FromResult(new PaymentResultDto { Success = true });
    }

    private static string HmacSha512(string key, string data)
    {
        using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

    private static string HmacSha256(string key, string data)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
}
