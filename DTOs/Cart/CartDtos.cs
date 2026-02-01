using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.Cart;

public class CartDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<CartItemDto> Items { get; set; } = [];
    public decimal Total => Items.Sum(i => i.SubTotal);
    public int TotalItems => Items.Sum(i => i.Quantity);
}

public class CartItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public string? ProductImageUrl { get; set; }
    public int? VariantId { get; set; }
    public string? Size { get; set; }
    public string? Color { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal SubTotal => Quantity * UnitPrice;
}

public class AddToCartDto
{
    [Required]
    public int ProductId { get; set; }
    
    public int? VariantId { get; set; }
    
    [Range(1, 100)]
    public int Quantity { get; set; } = 1;
}

public class UpdateCartItemDto
{
    [Required]
    public int CartItemId { get; set; }
    
    [Range(1, 100)]
    public int Quantity { get; set; }
}
