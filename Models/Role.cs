using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models;

public class Role : BaseModel
{
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    [MaxLength(200)]
    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = [];
}
