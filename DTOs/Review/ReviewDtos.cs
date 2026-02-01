using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.Review;

public class ReviewDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public int UserId { get; set; }
    public string UserName { get; set; } = "";
    public string? UserAvatarUrl { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public bool IsApproved { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ReviewCreateDto
{
    [Required]
    public int ProductId { get; set; }
    
    [Required, Range(1, 5)]
    public int Rating { get; set; }
    
    [MaxLength(1000)]
    public string? Comment { get; set; }
}

public class ReviewUpdateDto
{
    [Required, Range(1, 5)]
    public int Rating { get; set; }
    
    [MaxLength(1000)]
    public string? Comment { get; set; }
}
