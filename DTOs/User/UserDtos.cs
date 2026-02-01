using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.User;

public class UserDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string? PhoneNumber { get; set; }
    public string? AvatarUrl { get; set; }
    public bool IsActive { get; set; }
    public string RoleName { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
}

public class UserProfileDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string? PhoneNumber { get; set; }
    public string? AvatarUrl { get; set; }
    public List<AddressDto> Addresses { get; set; } = [];
}

public class UserUpdateDto
{
    [Required, MaxLength(100)]
    public required string FullName { get; set; }
    
    [Phone]
    public string? PhoneNumber { get; set; }
    
    [Url]
    public string? AvatarUrl { get; set; }
}

public class AddressDto
{
    public int Id { get; set; }
    public string RecipientName { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string StreetAddress { get; set; } = "";
    public string? Ward { get; set; }
    public string District { get; set; } = "";
    public string City { get; set; } = "";
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public bool IsDefault { get; set; }
}

public class AddressCreateDto
{
    [Required, MaxLength(100)]
    public required string RecipientName { get; set; }
    
    [Required, Phone]
    public required string PhoneNumber { get; set; }
    
    [Required, MaxLength(200)]
    public required string StreetAddress { get; set; }
    
    [MaxLength(100)]
    public string? Ward { get; set; }
    
    [Required, MaxLength(100)]
    public required string District { get; set; }
    
    [Required, MaxLength(100)]
    public required string City { get; set; }
    
    [MaxLength(100)]
    public string? Country { get; set; } = "Vietnam";
    
    [MaxLength(20)]
    public string? PostalCode { get; set; }
    
    public bool IsDefault { get; set; }
}
