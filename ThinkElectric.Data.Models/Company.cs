namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ThinkElectric.Common.EntityValidationConstants.Company;

public class Company
{
    public Company()
    {
        Id = Guid.NewGuid();
        Products = new HashSet<Product>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(EmailMaxLength)]
    public string Email { get; set; } = null!;

    [Required]
    [MaxLength(PhoneNumberMaxLength)]
    public string PhoneNumber { get; set; } = null!;

    [MaxLength(WebSiteMaxLength)]
    public string? WebSite { get; set; }

    [Required]
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; } = null!;

    public DateTime FoundedDate { get; set; }

    public double Rating { get; set; }

    public string? LogoUrl { get; set; }


    public Address Address { get; set; } = null!;

    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;

    public ICollection<Product> Products { get; set; }
}
