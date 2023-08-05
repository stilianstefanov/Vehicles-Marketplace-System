namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

using static Common.EntityValidationConstants.User;

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

    public Guid? AddressId { get; set; }

    public Address? Address { get; set; }

    public Company? Company { get; set; }

    public Cart? Cart { get; set; }

    public ICollection<Review> Reviews { get; set; }

    public ICollection<Order> Orders { get; set; }
}