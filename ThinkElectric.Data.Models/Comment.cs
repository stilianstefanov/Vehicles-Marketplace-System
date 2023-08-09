namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Common.EntityValidationConstants.Comment;

public class Comment
{
    public Comment()
    {
        Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(ContentMaxLength)]
    public string Content { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public bool IsDeleted { get; set; }


    [Required]
    public Guid PostId { get; set; }

    public Post Post { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;
}
