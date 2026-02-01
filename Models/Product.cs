using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models;

public class Product : BaseModel
{
    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? DiscountPrice { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Brand { get; set; }

    [MaxLength(50)]
    public string? SKU { get; set; }

    [MaxLength(500)]
    public string? MainImageUrl { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsFeatured { get; set; }

    public int CategoryId { get; set; }

    public virtual Category? Category { get; set; }
    public virtual ICollection<ProductImage> ProductImages { get; set; } = [];
    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = [];
    public virtual ICollection<Review> Reviews { get; set; } = [];
    public virtual ICollection<CartItem> CartItems { get; set; } = [];
    public virtual ICollection<OrderItem> OrderItems { get; set; } = [];
    public virtual ICollection<WishlistItem> WishlistItems { get; set; } = [];
}
