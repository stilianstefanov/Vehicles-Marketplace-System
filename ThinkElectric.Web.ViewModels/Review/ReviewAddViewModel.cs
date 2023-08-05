namespace ThinkElectric.Web.ViewModels.Review;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.Review;

public class ReviewAddViewModel
{
    [Required]
    [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
    public string Content { get; set; } = null!;

    [Range(RatingMinValue, RatingMaxValue)]
    public int Rating { get; set; }
}
