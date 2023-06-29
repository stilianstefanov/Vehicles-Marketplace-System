namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations;
using static ThinkElectric.Common.EntityValidationConstants.Category;

public class Category
{
    public Category()
    {
        Products = new HashSet<Product>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    public ICollection<Product> Products { get; set; }
}
