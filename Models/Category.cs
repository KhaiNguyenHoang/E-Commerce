using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models;

public class Category : BaseModel
{
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [MaxLength(255)]
    public string? ImageUrl { get; set; }

    public bool IsActive { get; set; } = true;

    public virtual ICollection<Product> Products { get; set; } = [];
}
