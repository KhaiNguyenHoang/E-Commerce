using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models;

public enum OrderStatus
{
    Pending,
    Confirmed,
    Processing,
    Shipped,
    Delivered,
    Cancelled,
    Refunded
}

public enum PaymentStatus
{
    Pending,
    Paid,
    Failed,
    Refunded
}

public enum PaymentMethod
{
    COD,
    Stripe
}

public class Order : BaseModel
{
    [Required]
    [MaxLength(50)]
    public required string OrderNumber { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.COD;

    [Column(TypeName = "decimal(18,2)")]
    public decimal SubTotal { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ShippingFee { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountAmount { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Required]
    [MaxLength(100)]
    public required string ShippingName { get; set; }

    [Required]
    [MaxLength(20)]
    public required string ShippingPhone { get; set; }

    [Required]
    [MaxLength(500)]
    public required string ShippingAddress { get; set; }

    [MaxLength(500)]
    public string? Note { get; set; }

    public DateTime? ShippedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }

    public int UserId { get; set; }
    public int? CouponId { get; set; }

    public virtual User? User { get; set; }
    public virtual Coupon? Coupon { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = [];
}
