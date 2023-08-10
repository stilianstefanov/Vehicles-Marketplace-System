namespace ThinkElectric.Web.ViewModels.Comment;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.Comment;

public class CommentEditViewModel
{
    [Required]
    [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
    public string Content { get; set; } = null!;
}
