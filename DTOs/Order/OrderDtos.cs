using System.ComponentModel.DataAnnotations;
using E_Commerce.Models;

namespace E_Commerce.DTOs.Order;

public class OrderDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = "";
    public OrderStatus Status { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public string PaymentMethod { get; set; } = "";
    public decimal SubTotal { get; set; }
    public decimal ShippingFee { get; set; }
    public decimal TotalAmount { get; set; }
    public string ShippingName { get; set; } = "";
    public string ShippingPhone { get; set; } = "";
    public string ShippingAddress { get; set; } = "";
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderItemDto> Items { get; set; } = [];
}

public class OrderItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public string? ProductImageUrl { get; set; }
    public string? Size { get; set; }
    public string? Color { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}

public class OrderCreateDto
{
    [Required]
    public required string ShippingName { get; set; }
    
    [Required, Phone]
    public required string ShippingPhone { get; set; }
    
    [Required]
    public required string ShippingAddress { get; set; }
    
    [Required]
    public required string PaymentMethod { get; set; }
    
    public string? Note { get; set; }
}

public class OrderStatusUpdateDto
{
    [Required]
    public OrderStatus Status { get; set; }
}

public class PaymentStatusUpdateDto
{
    [Required]
    public PaymentStatus PaymentStatus { get; set; }
}
