namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ThinkElectric.Common.EntityValidationConstants.Accessory;

public class Accessory
{
    public Accessory()
    {
        Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(BrandMaxLength)]
    public string Brand { get; set; } = null!;

    [Required]
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; } = null!;

    [Required]
    [MaxLength(CompatibleBrandMaxLength)]
    public string CompatibleBrand { get; set; } = null!;

    [Required]
    [MaxLength(CompatibleModelMaxLength)]
    public string CompatibleModel { get; set; } = null!;


    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }

    public Product Product { get; set; } = null!;
}
