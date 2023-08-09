namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Common.EntityValidationConstants.Post;

public class Post
{
    public Post()
    {
        Id = Guid.NewGuid();
        Comments = new HashSet<Comment>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(TitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(ContentMaxLength)]
    public string Content { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;

    public ICollection<Comment> Comments { get; set; }
}
