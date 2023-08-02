namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
    public Order()
    {
        Id = Guid.NewGuid();
        OrderItems = new HashSet<OrderItem>();
    }

    [Key]
    public Guid Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool IsFulfilled { get; set; }

    public bool IsConfirmedByUser { get; set; }



    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; }
}
