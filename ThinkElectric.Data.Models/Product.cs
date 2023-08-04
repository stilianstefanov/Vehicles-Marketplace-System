namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Enums.Product;
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
    [MaxLength(ImageIdMaxLength)]
    public string ImageId { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedOn { get; set; }

    public ProductType ProductType { get; set; }

    public bool IsDeleted { get; set; }



    [ForeignKey(nameof(Company))]
    public Guid CompanyId { get; set; }

    public Company Company { get; set; } = null!;

    public Scooter? Scooter { get; set; }

    public Bike? Bike { get; set; }

    public Accessory? Accessory { get; set; }

    public ICollection<Review> Reviews { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }

    public ICollection<CartItem> CartItems { get; set; }
}
