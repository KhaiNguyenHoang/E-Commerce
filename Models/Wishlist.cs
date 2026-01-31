namespace E_Commerce.Models;

public class Wishlist : BaseModel
{
    public int UserId { get; set; }

    public virtual User? User { get; set; }
    public virtual ICollection<WishlistItem> WishlistItems { get; set; } = [];
}
