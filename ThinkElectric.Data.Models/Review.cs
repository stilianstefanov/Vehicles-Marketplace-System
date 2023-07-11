namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ThinkElectric.Common.EntityValidationConstants.Review;

public class Review
{
    public Review()
    {
        Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(ContentMaxLength)]
    public string Content { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public double Rating { get; set; }


    [ForeignKey(nameof(Product))]
    public Guid? ProductId { get; set; }

    public Product? Product { get; set; } = null!;

    [ForeignKey(nameof(Company))]
    public Guid? CompanyId { get; set; }

    public Company? Company { get; set; } = null!;

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;
}
