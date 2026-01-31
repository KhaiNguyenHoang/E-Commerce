namespace E_Commerce.Models;

public class WishlistItem : BaseModel
{
    public int WishlistId { get; set; }
    public int ProductId { get; set; }

    public virtual Wishlist? Wishlist { get; set; }
    public virtual Product? Product { get; set; }
}
