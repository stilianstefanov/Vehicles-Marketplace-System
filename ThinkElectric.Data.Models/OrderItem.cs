namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderItem
{
    public OrderItem()
    {
        Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    public int Quantity { get; set; }

    public bool IsFulfilled { get; set; }


    [ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }

    public Order Order { get; set; } = null!;

    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }

    public Product Product { get; set; } = null!;
}
