using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace E_Commerce.Services;

public class StripeService : IStripeService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<StripeService> _logger;

    public StripeService(IConfiguration configuration, ApplicationDbContext dbContext, ILogger<StripeService> logger)
    {
        _configuration = configuration;
        _dbContext = dbContext;
        _logger = logger;

        // Configure Stripe API key
        StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
    }

    public async Task<string> CreateCheckoutSessionAsync(int orderId, string successUrl, string cancelUrl)
    {
        var order = await _dbContext.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.Id == orderId)
            ?? throw new Exception("Order not found");

        // Convert VND to USD cents (approximate rate: 1 USD = 25,000 VND)
        // Stripe uses cents, so multiply by 100
        const decimal VND_TO_USD_RATE = 25000m;
        
        long ConvertToUsdCents(decimal vndAmount)
        {
            var usdAmount = vndAmount / VND_TO_USD_RATE;
            return (long)(usdAmount * 100); // Convert to cents
        }

        var lineItems = order.OrderItems.Select(item => new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                Currency = "usd",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = item.Product?.Name ?? "Product",
                    Description = $"Size: {item.Size}, Color: {item.Color}"
                },
                UnitAmount = ConvertToUsdCents(item.UnitPrice)
            },
            Quantity = item.Quantity
        }).ToList();

        // Add shipping fee as line item
        if (order.ShippingFee > 0)
        {
            lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Shipping Fee"
                    },
                    UnitAmount = ConvertToUsdCents(order.ShippingFee)
                },
                Quantity = 1
            });
        }

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = ["card"],
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = $"{successUrl}?session_id={{CHECKOUT_SESSION_ID}}",
            CancelUrl = cancelUrl,
            Metadata = new Dictionary<string, string>
            {
                { "order_id", orderId.ToString() }
            },
            CustomerEmail = order.User?.Email
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        // Store session ID
        order.Note = (order.Note ?? "") + $" [Stripe: {session.Id}]";
        await _dbContext.SaveChangesAsync();

        return session.Url ?? throw new Exception("Failed to create checkout session");
    }

    public async Task<bool> HandleWebhookAsync(string json, string signature)
    {
        try
        {
            var webhookSecret = _configuration["Stripe:WebhookSecret"];
            var stripeEvent = EventUtility.ConstructEvent(json, signature, webhookSecret);

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Session;
                if (session?.Metadata.TryGetValue("order_id", out var orderIdStr) == true)
                {
                    if (int.TryParse(orderIdStr, out var orderId))
                    {
                        var order = await _dbContext.Orders.FindAsync(orderId);
                        if (order != null)
                        {
                            order.PaymentStatus = PaymentStatus.Paid;
                            order.Status = OrderStatus.Confirmed;
                            order.UpdatedAt = DateTime.UtcNow;
                            await _dbContext.SaveChangesAsync();

                            _logger.LogInformation("Payment confirmed for order {OrderId}", orderId);
                            return true;
                        }
                    }
                }
            }

            return true;
        }
        catch (StripeException ex)
        {
            _logger.LogError(ex, "Stripe webhook error");
            return false;
        }
    }
}
