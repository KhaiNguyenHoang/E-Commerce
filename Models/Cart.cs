namespace E_Commerce.Models;

public class Cart : BaseModel
{
    public int UserId { get; set; }

    public virtual User? User { get; set; }
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
