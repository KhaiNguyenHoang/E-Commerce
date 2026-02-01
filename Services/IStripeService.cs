namespace E_Commerce.Services;

public interface IStripeService
{
    Task<string> CreateCheckoutSessionAsync(int orderId, string successUrl, string cancelUrl);
    Task<bool> HandleWebhookAsync(string json, string signature);
}
