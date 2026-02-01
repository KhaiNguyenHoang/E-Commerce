using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.Auth;

public class LoginDto
{
    [Required, EmailAddress]
    public required string Email { get; set; }
    
    [Required, MinLength(6)]
    public required string Password { get; set; }
}

public class RegisterDto
{
    [Required, MaxLength(100)]
    public required string FullName { get; set; }
    
    [Required, EmailAddress]
    public required string Email { get; set; }
    
    [Phone]
    public string? PhoneNumber { get; set; }
    
    [Required, MinLength(6)]
    public required string Password { get; set; }
    
    [Required, Compare(nameof(Password))]
    public required string ConfirmPassword { get; set; }
}

public class AuthResponseDto
{
    public int UserId { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Role { get; set; } = "";
    public string? AvatarUrl { get; set; }
}

public class ChangePasswordDto
{
    [Required]
    public required string CurrentPassword { get; set; }
    
    [Required, MinLength(6)]
    public required string NewPassword { get; set; }
    
    [Required, Compare(nameof(NewPassword))]
    public required string ConfirmPassword { get; set; }
}
