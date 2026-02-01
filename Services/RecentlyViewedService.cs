namespace E_Commerce.Services;

public interface IRecentlyViewedService
{
    void AddProduct(int productId);
    IEnumerable<int> GetRecentProductIds(int count = 10);
    void Clear();
}

public class RecentlyViewedService : IRecentlyViewedService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string CookieName = "RecentlyViewed";
    private const int MaxItems = 20;

    public RecentlyViewedService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void AddProduct(int productId)
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null) return;

        var ids = GetRecentProductIds(MaxItems).ToList();
        ids.Remove(productId);
        ids.Insert(0, productId);

        if (ids.Count > MaxItems)
            ids = ids.Take(MaxItems).ToList();

        var cookieValue = string.Join(",", ids);
        context.Response.Cookies.Append(CookieName, cookieValue, new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(30),
            HttpOnly = true,
            IsEssential = true
        });
    }

    public IEnumerable<int> GetRecentProductIds(int count = 10)
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null) return [];

        var cookieValue = context.Request.Cookies[CookieName];
        if (string.IsNullOrEmpty(cookieValue)) return [];

        return cookieValue.Split(',')
            .Where(s => int.TryParse(s, out _))
            .Select(int.Parse)
            .Take(count);
    }

    public void Clear()
    {
        var context = _httpContextAccessor.HttpContext;
        context?.Response.Cookies.Delete(CookieName);
    }
}
