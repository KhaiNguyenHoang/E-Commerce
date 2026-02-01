using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.Product;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public string Brand { get; set; } = "";
    public string? SKU { get; set; }
    public string? MainImageUrl { get; set; }
    public bool IsActive { get; set; }
    public bool IsFeatured { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ProductVariantDto> Variants { get; set; } = [];
    public List<ProductImageDto> Images { get; set; } = [];
}

public class ProductCreateDto
{
    [Required, MaxLength(200)]
    public required string Name { get; set; }
    
    [MaxLength(2000)]
    public string? Description { get; set; }
    
    [Required, Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
    
    public decimal? DiscountPrice { get; set; }
    
    [Required, MaxLength(100)]
    public required string Brand { get; set; }
    
    [MaxLength(50)]
    public string? SKU { get; set; }
    
    [Url]
    public string? MainImageUrl { get; set; }
    
    public bool IsActive { get; set; } = true;
    public bool IsFeatured { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
}

public class ProductUpdateDto : ProductCreateDto
{
    public int Id { get; set; }
}

public class ProductVariantDto
{
    public int Id { get; set; }
    public string Size { get; set; } = "";
    public string Color { get; set; } = "";
    public int StockQuantity { get; set; }
    public bool IsAvailable { get; set; }
}

public class ProductVariantCreateDto
{
    [Required, MaxLength(20)]
    public required string Size { get; set; }
    
    [Required, MaxLength(50)]
    public required string Color { get; set; }
    
    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }
    
    public bool IsAvailable { get; set; } = true;
}

public class ProductImageDto
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = "";
    public string? AltText { get; set; }
    public int DisplayOrder { get; set; }
}

public class ProductImageCreateDto
{
    [Required, Url]
    public required string ImageUrl { get; set; }
    
    [MaxLength(200)]
    public string? AltText { get; set; }
    
    public int DisplayOrder { get; set; }
}

public class ProductFilterDto
{
    public int? CategoryId { get; set; }
    public string? Search { get; set; }
    public string? Brand { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? SortBy { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 12;
}
