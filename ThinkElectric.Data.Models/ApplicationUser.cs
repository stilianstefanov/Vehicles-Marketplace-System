namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser()
    {
        Id = Guid.NewGuid();
        Reviews = new HashSet<Review>();
        Orders = new HashSet<Order>();
    }

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