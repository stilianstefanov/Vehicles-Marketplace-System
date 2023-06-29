namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ThinkElectric.Common.EntityValidationConstants.Product;

public class Product
{
    public Product()
    {
        Id = Guid.NewGuid();
        Reviews = new HashSet<Review>();
        OrderItems = new HashSet<OrderItem>();
        CartItems = new HashSet<CartItem>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(ImageUrlMaxLength)]
    public string ImageUrl { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public double Rating { get; set; }

    public DateTime CreatedOn { get; set; }


    [ForeignKey(nameof(Company))]
    public Guid CompanyId { get; set; }

    public Company Company { get; set; } = null!;

    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public Guid? ScooterId { get; set; }

    public Scooter? Scooter { get; set; }

    public Guid? BikeId { get; set; }

    public Bike? Bike { get; set; }

    public Guid? AccessoryId { get; set; }

    public Accessory? Accessory { get; set; }

    public ICollection<Review> Reviews { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }

    public ICollection<CartItem> CartItems { get; set; }
}
