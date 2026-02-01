using E_Commerce.DTOs.Coupon;
using E_Commerce.Models;
using E_Commerce.Repositories;

namespace E_Commerce.Services;

public interface ICouponService
{
    Task<IEnumerable<Coupon>> GetAllAsync();
    Task<IEnumerable<Coupon>> GetActiveAsync();
    Task<Coupon?> GetByIdAsync(int id);
    Task<Coupon?> GetByCodeAsync(string code);
    Task<CouponResultDto> ValidateCouponAsync(string code, decimal orderTotal, int userId);
    Task<bool> ValidateCouponAsync(string code, decimal orderTotal); // Simple validation
    Task<decimal> CalculateDiscountAsync(string code, decimal orderTotal);
    Task IncrementUsageAsync(int couponId);
    Task<CouponResultDto> ApplyCouponAsync(string code, decimal orderTotal, int userId, int orderId);
    Task CreateAsync(Coupon coupon);
    Task UpdateAsync(Coupon coupon);
    Task DeleteAsync(int id);
}

public class CouponService : ICouponService
{
    private readonly ICouponRepository _couponRepository;
    private readonly Data.ApplicationDbContext _context;

    public CouponService(ICouponRepository couponRepository, Data.ApplicationDbContext context)
    {
        _couponRepository = couponRepository;
        _context = context;
    }

    public async Task<IEnumerable<Coupon>> GetAllAsync() => await _couponRepository.GetAllAsync();
    public async Task<IEnumerable<Coupon>> GetActiveAsync() => await _couponRepository.GetActiveAsync();
    public async Task<Coupon?> GetByIdAsync(int id) => await _couponRepository.GetByIdAsync(id);
    public async Task<Coupon?> GetByCodeAsync(string code) => await _couponRepository.GetByCodeAsync(code);

    public async Task<CouponResultDto> ValidateCouponAsync(string code, decimal orderTotal, int userId)
    {
        var coupon = await _couponRepository.GetByCodeAsync(code);
        
        if (coupon == null)
            return new CouponResultDto { IsValid = false, Message = "Coupon not found" };
        
        var now = DateTime.UtcNow;
        
        if (!coupon.IsActive)
            return new CouponResultDto { IsValid = false, Message = "Coupon is inactive" };
        
        if (now < coupon.StartDate)
            return new CouponResultDto { IsValid = false, Message = "Coupon is not yet active" };
        
        if (now > coupon.EndDate)
            return new CouponResultDto { IsValid = false, Message = "Coupon has expired" };
        
        if (coupon.UsageLimit.HasValue && coupon.UsedCount >= coupon.UsageLimit.Value)
            return new CouponResultDto { IsValid = false, Message = "Coupon usage limit reached" };
        
        if (coupon.MinimumOrderAmount.HasValue && orderTotal < coupon.MinimumOrderAmount.Value)
            return new CouponResultDto { IsValid = false, Message = $"Minimum order amount is {coupon.MinimumOrderAmount:C}" };
        
        // Check if user already used this coupon
        var alreadyUsed = _context.Set<CouponUsage>().Any(cu => cu.CouponId == coupon.Id && cu.UserId == userId);
        if (alreadyUsed)
            return new CouponResultDto { IsValid = false, Message = "You have already used this coupon" };
        
        // Calculate discount
        decimal discountAmount = coupon.DiscountType == DiscountType.Percentage
            ? orderTotal * (coupon.DiscountValue / 100)
            : coupon.DiscountValue;
        
        if (coupon.MaximumDiscountAmount.HasValue && discountAmount > coupon.MaximumDiscountAmount.Value)
            discountAmount = coupon.MaximumDiscountAmount.Value;
        
        return new CouponResultDto
        {
            IsValid = true,
            Message = "Coupon applied successfully",
            DiscountAmount = discountAmount,
            NewTotal = orderTotal - discountAmount,
            Coupon = new CouponDto
            {
                Id = coupon.Id,
                Code = coupon.Code,
                Description = coupon.Description,
                DiscountType = coupon.DiscountType.ToString(),
                DiscountValue = coupon.DiscountValue
            }
        };
    }

    public async Task<CouponResultDto> ApplyCouponAsync(string code, decimal orderTotal, int userId, int orderId)
    {
        var result = await ValidateCouponAsync(code, orderTotal, userId);
        
        if (!result.IsValid) return result;
        
        var coupon = await _couponRepository.GetByCodeAsync(code);
        if (coupon == null) return result;
        
        // Record usage
        var usage = new CouponUsage
        {
            CouponId = coupon.Id,
            UserId = userId,
            OrderId = orderId,
            DiscountAmount = result.DiscountAmount
        };
        
        _context.Set<CouponUsage>().Add(usage);
        coupon.UsedCount++;
        await _context.SaveChangesAsync();
        
        return result;
    }

    // Simple validation without user check (for anonymous/checkout preview)
    public async Task<bool> ValidateCouponAsync(string code, decimal orderTotal)
    {
        var coupon = await _couponRepository.GetByCodeAsync(code);
        if (coupon == null) return false;

        var now = DateTime.UtcNow;
        if (!coupon.IsActive) return false;
        if (now < coupon.StartDate || now > coupon.EndDate) return false;
        if (coupon.UsageLimit.HasValue && coupon.UsedCount >= coupon.UsageLimit.Value) return false;
        if (coupon.MinimumOrderAmount.HasValue && orderTotal < coupon.MinimumOrderAmount.Value) return false;

        return true;
    }

    public async Task<decimal> CalculateDiscountAsync(string code, decimal orderTotal)
    {
        var coupon = await _couponRepository.GetByCodeAsync(code);
        if (coupon == null) return 0;

        decimal discountAmount = coupon.DiscountType == DiscountType.Percentage
            ? orderTotal * (coupon.DiscountValue / 100)
            : coupon.DiscountValue;

        if (coupon.MaximumDiscountAmount.HasValue && discountAmount > coupon.MaximumDiscountAmount.Value)
            discountAmount = coupon.MaximumDiscountAmount.Value;

        return discountAmount;
    }

    public async Task IncrementUsageAsync(int couponId)
    {
        var coupon = await _couponRepository.GetByIdAsync(couponId);
        if (coupon != null)
        {
            coupon.UsedCount++;
            await _couponRepository.UpdateAsync(coupon);
        }
    }

    public async Task CreateAsync(Coupon coupon) => await _couponRepository.AddAsync(coupon);
    public async Task UpdateAsync(Coupon coupon) => await _couponRepository.UpdateAsync(coupon);
    public async Task DeleteAsync(int id)
    {
        var coupon = await _couponRepository.GetByIdAsync(id);
        if (coupon != null) await _couponRepository.DeleteAsync(coupon);
    }
}
