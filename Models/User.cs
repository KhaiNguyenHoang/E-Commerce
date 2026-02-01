using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models;

public class User : BaseModel
{
    [Required]
    [MaxLength(100)]
    public required string FullName { get; set; }

    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Password { get; set; }

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [MaxLength(500)]
    public string? AvatarUrl { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime? LastLoginAt { get; set; }

    [MaxLength(255)]
    public string? ResetPasswordToken { get; set; }
    public DateTime? ResetPasswordTokenExpiry { get; set; }

    public int RoleId { get; set; }

    public virtual Role? Role { get; set; }
    public virtual ICollection<Address> Addresses { get; set; } = [];
    public virtual ICollection<Order> Orders { get; set; } = [];
    public virtual ICollection<Review> Reviews { get; set; } = [];
    public virtual Cart? Cart { get; set; }
    public virtual Wishlist? Wishlist { get; set; }
}
