using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.Category;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public int ProductCount { get; set; }
}

public class CategoryCreateDto
{
    [Required, MaxLength(100)]
    public required string Name { get; set; }
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    [Url]
    public string? ImageUrl { get; set; }
    
    public bool IsActive { get; set; } = true;
}

public class CategoryUpdateDto : CategoryCreateDto
{
    public int Id { get; set; }
}
