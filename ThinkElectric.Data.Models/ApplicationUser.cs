namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static ThinkElectric.Common.EntityValidationConstants.User;

public class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser()
    {
        Id = Guid.NewGuid();
        Reviews = new HashSet<Review>();
        Orders = new HashSet<Order>();
    }

    [Required]
    [MaxLength(FirstNameMaxLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(LastNameMaxLength)]
    public string LastName { get; set; } = null!;

    [ForeignKey(nameof(Address))]
    public Guid AddressId { get; set; }

    public Address Address { get; set; } = null!;

    public Guid? CompanyId { get; set; }

    public Company? Company { get; set; }

    [ForeignKey(nameof(Cart))]
    public Guid? CartId { get; set; }

    public Cart? Cart { get; set; }

    public ICollection<Review> Reviews { get; set; }

    public ICollection<Order> Orders { get; set; }
}