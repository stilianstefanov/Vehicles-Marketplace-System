namespace ThinkElectric.Web.ViewModels.Post;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.Post;

public class PostEditViewModel
{
    [Required]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
    public string Content { get; set; } = null!;
}
