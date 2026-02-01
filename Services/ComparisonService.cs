namespace E_Commerce.Services;

public interface IComparisonService
{
    void AddProduct(int productId);
    void RemoveProduct(int productId);
    IEnumerable<int> GetProductIds();
    void Clear();
    bool CanAdd { get; }
}

public class ComparisonService : IComparisonService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string SessionKey = "CompareProducts";
    private const int MaxItems = 4;

    public ComparisonService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool CanAdd => GetProductIds().Count() < MaxItems;

    public void AddProduct(int productId)
    {
        var session = _httpContextAccessor.HttpContext?.Session;
        if (session == null) return;

        var ids = GetProductIds().ToList();
        if (ids.Contains(productId) || ids.Count >= MaxItems) return;

        ids.Add(productId);
        session.SetString(SessionKey, string.Join(",", ids));
    }

    public void RemoveProduct(int productId)
    {
        var session = _httpContextAccessor.HttpContext?.Session;
        if (session == null) return;

        var ids = GetProductIds().ToList();
        ids.Remove(productId);
        session.SetString(SessionKey, string.Join(",", ids));
    }

    public IEnumerable<int> GetProductIds()
    {
        var session = _httpContextAccessor.HttpContext?.Session;
        if (session == null) return [];

        var value = session.GetString(SessionKey);
        if (string.IsNullOrEmpty(value)) return [];

        return value.Split(',')
            .Where(s => int.TryParse(s, out _))
            .Select(int.Parse);
    }

    public void Clear()
    {
        var session = _httpContextAccessor.HttpContext?.Session;
        session?.Remove(SessionKey);
    }
}
