using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models;

public class OrderItem : BaseModel
{
    [Required]
    [MaxLength(200)]
    public required string ProductName { get; set; }

    [MaxLength(255)]
    public string? ProductImageUrl { get; set; }

    [MaxLength(20)]
    public string? Size { get; set; }

    [MaxLength(50)]
    public string? Color { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int? ProductVariantId { get; set; }

    public virtual Order? Order { get; set; }
    public virtual Product? Product { get; set; }
    public virtual ProductVariant? ProductVariant { get; set; }
}
