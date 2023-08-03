namespace ThinkElectric.Web.ViewModels.Review;

public class ReviewMineViewModel
{
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string CreatedOn { get; set; } = null!;

    public double Rating { get; set; }

    public string? ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? CompanyId { get; set; }

    public string? CompanyName { get; set; }
}
