namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CartItem
{
    [Key]
    public Guid Id { get; set; }

    public int Quantity { get; set; }


    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }

    public Product Product { get; set; } = null!;

    [ForeignKey(nameof(Cart))]
    public Guid CartId { get; set; }

    public Cart Cart { get; set; } = null!;

    public decimal TotalSum()
       => Quantity * Product.Price;
}
