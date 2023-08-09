namespace ThinkElectric.Web.ViewModels.Review;

public class ReviewCompanyAdminViewModel
{
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string CreatedOn { get; set; } = null!;

    public double Rating { get; set; }

    public string CompanyId { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string UserFullName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;
}
