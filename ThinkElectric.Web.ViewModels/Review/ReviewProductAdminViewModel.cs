namespace ThinkElectric.Web.ViewModels.Review;

public class ReviewProductAdminViewModel
{
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string CreatedOn { get; set; } = null!;

    public double Rating { get; set; }

    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string UserFullName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;
}
