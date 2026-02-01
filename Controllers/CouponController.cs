using E_Commerce.Models;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers;

public class CouponController : BaseController
{
    private readonly ICouponService _couponService;

    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    // POST: /Coupon/Validate
    [HttpPost]
    public async Task<IActionResult> Validate([FromBody] string code, [FromBody] decimal orderTotal)
    {
        var userId = await GetCurrentUserIdAsync();
        if (!userId.HasValue)
            return Json(new { success = false, message = "Please login first" });

        var result = await _couponService.ValidateCouponAsync(code, orderTotal, userId.Value);
        return Json(new
        {
            success = result.IsValid,
            message = result.Message,
            discountAmount = result.DiscountAmount,
            newTotal = result.NewTotal
        });
    }

    // GET: /Coupon/Manage (Admin)
    [RequireRole("Admin")]
    public async Task<IActionResult> Manage()
    {
        var coupons = await _couponService.GetAllAsync();
        return View(coupons);
    }

    // GET: /Coupon/Create (Admin)
    [RequireRole("Admin")]
    public IActionResult Create() => View();

    // POST: /Coupon/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireRole("Admin")]
    public async Task<IActionResult> Create(Coupon coupon)
    {
        coupon.Code = coupon.Code.ToUpper();
        await _couponService.CreateAsync(coupon);
        TempData["Success"] = "Coupon created successfully";
        return RedirectToAction(nameof(Manage));
    }

    // GET: /Coupon/Edit/5 (Admin)
    [RequireRole("Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var coupon = await _couponService.GetByIdAsync(id);
        if (coupon == null) return NotFound();
        return View(coupon);
    }

    // POST: /Coupon/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireRole("Admin")]
    public async Task<IActionResult> Edit(int id, Coupon coupon)
    {
        coupon.Id = id;
        coupon.Code = coupon.Code.ToUpper();
        await _couponService.UpdateAsync(coupon);
        TempData["Success"] = "Coupon updated successfully";
        return RedirectToAction(nameof(Manage));
    }

    // POST: /Coupon/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireRole("Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _couponService.DeleteAsync(id);
        TempData["Success"] = "Coupon deleted";
        return RedirectToAction(nameof(Manage));
    }
}
