using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models;

public class Address : BaseModel
{
    [Required]
    [MaxLength(100)]
    public required string RecipientName { get; set; }

    [Required]
    [MaxLength(20)]
    public required string PhoneNumber { get; set; }

    [Required]
    [MaxLength(255)]
    public required string StreetAddress { get; set; }

    [MaxLength(100)]
    public string? Ward { get; set; }

    [Required]
    [MaxLength(100)]
    public required string District { get; set; }

    [Required]
    [MaxLength(100)]
    public required string City { get; set; }

    [MaxLength(100)]
    public string? Country { get; set; } = "Vietnam";

    [MaxLength(20)]
    public string? PostalCode { get; set; }

    public bool IsDefault { get; set; }

    public int UserId { get; set; }

    public virtual User? User { get; set; }
}
