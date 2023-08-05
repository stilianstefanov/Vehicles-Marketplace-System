namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Cart
{
    public Cart()
    {
        Id = Guid.NewGuid();
        CartItems = new HashSet<CartItem>();
    }

    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;

    public ICollection<CartItem> CartItems { get; set; }
}
