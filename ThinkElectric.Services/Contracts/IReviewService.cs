namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Review;

public interface IReviewService
{
    Task<IEnumerable<ReviewViewModel>> GetReviewsByCompanyIdAsync(string companyId);
    Task<IEnumerable<ReviewViewModel>> GetReviewsByProductIdAsync(string productId);
}
