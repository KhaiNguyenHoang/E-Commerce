using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models;

public class Review : BaseModel
{
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    [MaxLength(1000)]
    public string? Comment { get; set; }

    public bool IsVerifiedPurchase { get; set; }

    public bool IsApproved { get; set; }

    public int UserId { get; set; }
    public int ProductId { get; set; }

    public virtual User? User { get; set; }
    public virtual Product? Product { get; set; }
}
