namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Review;

public interface IReviewService
{
    Task<IEnumerable<ReviewViewModel>> GetReviewsByCompanyIdAsync(string companyId);
}
