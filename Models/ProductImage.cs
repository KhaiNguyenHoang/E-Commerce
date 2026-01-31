using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models;

public class ProductImage : BaseModel
{
    [Required]
    [MaxLength(255)]
    public required string ImageUrl { get; set; }

    [MaxLength(200)]
    public string? AltText { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsMain { get; set; }

    public int ProductId { get; set; }

    public virtual Product? Product { get; set; }
}
