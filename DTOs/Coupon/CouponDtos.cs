using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.Coupon;

public class CouponDto
{
    public int Id { get; set; }
    public string Code { get; set; } = "";
    public string? Description { get; set; }
    public string DiscountType { get; set; } = "";
    public decimal DiscountValue { get; set; }
    public decimal? MinimumOrderAmount { get; set; }
    public decimal? MaximumDiscountAmount { get; set; }
    public int? UsageLimit { get; set; }
    public int UsedCount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public bool IsValid { get; set; }
}

public class CouponCreateDto
{
    [Required, MaxLength(50)]
    public required string Code { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    [Required]
    public required string DiscountType { get; set; }
    
    [Required, Range(0.01, double.MaxValue)]
    public decimal DiscountValue { get; set; }
    
    public decimal? MinimumOrderAmount { get; set; }
    public decimal? MaximumDiscountAmount { get; set; }
    public int? UsageLimit { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    
    public bool IsActive { get; set; } = true;
}

public class ApplyCouponDto
{
    [Required]
    public required string Code { get; set; }
    
    [Required]
    public decimal OrderTotal { get; set; }
}

public class CouponResultDto
{
    public bool IsValid { get; set; }
    public string? Message { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal NewTotal { get; set; }
    public CouponDto? Coupon { get; set; }
}
