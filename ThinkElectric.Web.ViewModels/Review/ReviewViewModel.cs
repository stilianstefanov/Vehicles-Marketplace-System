namespace ThinkElectric.Web.ViewModels.Review;

public class ReviewViewModel
{
    public string Content { get; set; } = null!;

    public string CreatedOn { get; set; } = null!;

    public string? ModifiedOn { get; set; }

    public double Rating { get; set; }

    public string UserFullName { get; set; } = null!;
}
