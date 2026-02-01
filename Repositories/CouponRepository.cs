using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories;

public interface ICouponRepository
{
    Task<IEnumerable<Coupon>> GetAllAsync();
    Task<Coupon?> GetByIdAsync(int id);
    Task<Coupon?> GetByCodeAsync(string code);
    Task<IEnumerable<Coupon>> GetActiveAsync();
    Task AddAsync(Coupon coupon);
    Task UpdateAsync(Coupon coupon);
    Task DeleteAsync(Coupon coupon);
}

public class CouponRepository : ICouponRepository
{
    private readonly ApplicationDbContext _context;

    public CouponRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Coupon>> GetAllAsync()
    {
        return await _context.Coupons.ToListAsync();
    }

    public async Task<Coupon?> GetByIdAsync(int id)
    {
        return await _context.Coupons.FindAsync(id);
    }

    public async Task<Coupon?> GetByCodeAsync(string code)
    {
        return await _context.Coupons
            .FirstOrDefaultAsync(c => c.Code.ToLower() == code.ToLower());
    }

    public async Task<IEnumerable<Coupon>> GetActiveAsync()
    {
        var now = DateTime.UtcNow;
        return await _context.Coupons
            .Where(c => c.IsActive && c.StartDate <= now && c.EndDate >= now)
            .ToListAsync();
    }

    public async Task AddAsync(Coupon coupon)
    {
        await _context.Coupons.AddAsync(coupon);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Coupon coupon)
    {
        _context.Coupons.Update(coupon);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Coupon coupon)
    {
        _context.Coupons.Remove(coupon);
        await _context.SaveChangesAsync();
    }
}
