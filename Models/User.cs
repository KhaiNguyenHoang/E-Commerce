namespace E_Commerce.Models;

public class User : BaseModel
{
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required Role Role { get; set; }
    public required bool IsActive { get; set; }
}