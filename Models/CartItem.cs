using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models;

public class CartItem : BaseModel
{
    [Required]
    public int Quantity { get; set; } = 1;

    [MaxLength(20)]
    public string? SelectedSize { get; set; }

    [MaxLength(50)]
    public string? SelectedColor { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    // Foreign keys
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int? ProductVariantId { get; set; }

    // Navigation properties
    public virtual Cart? Cart { get; set; }
    public virtual Product? Product { get; set; }
    public virtual ProductVariant? ProductVariant { get; set; }
}
