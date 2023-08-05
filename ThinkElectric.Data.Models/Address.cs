namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations;

using static ThinkElectric.Common.EntityValidationConstants.Address;

public class Address
{
    public Address()
    {
        Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(CountryMaxLength)]
    public string Country { get; set; } = null!;

    [Required]
    [MaxLength(CityMaxLength)]
    public string City { get; set; } = null!;

    [Required]
    [MaxLength(StreetMaxLength)]
    public string Street { get; set; } = null!;

    [Required]
    [MaxLength(ZipCodeMaxLength)]
    public string ZipCode { get; set; } = null!;



    public ApplicationUser? User { get; set; }

    public Company? Company { get; set; }
}
