using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models;

public class ProductVariant : BaseModel
{
    [Required]
    [MaxLength(20)]
    public required string Size { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Color { get; set; }

    [MaxLength(20)]
    public string? ColorCode { get; set; }

    [Required]
    public int StockQuantity { get; set; }

    [MaxLength(50)]
    public string? VariantSKU { get; set; }

    public bool IsAvailable { get; set; } = true;

    public int ProductId { get; set; }

    public virtual Product? Product { get; set; }
}
