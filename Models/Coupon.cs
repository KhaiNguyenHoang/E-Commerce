using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models;

public enum DiscountType
{
    Percentage,
    FixedAmount
}

public class Coupon : BaseModel
{
    [Required]
    [MaxLength(50)]
    public required string Code { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    public DiscountType DiscountType { get; set; } = DiscountType.Percentage;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountValue { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? MinimumOrderAmount { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? MaximumDiscountAmount { get; set; }
    
    public int? UsageLimit { get; set; }
    
    public int UsedCount { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    // Navigation
    public virtual ICollection<CouponUsage>? CouponUsages { get; set; }
}

public class CouponUsage : BaseModel
{
    public int CouponId { get; set; }
    public virtual Coupon? Coupon { get; set; }
    
    public int UserId { get; set; }
    public virtual User? User { get; set; }
    
    public int OrderId { get; set; }
    public virtual Order? Order { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountAmount { get; set; }
}
