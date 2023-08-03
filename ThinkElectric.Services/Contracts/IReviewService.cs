namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Review;

public interface IReviewService
{
    Task<IEnumerable<ReviewViewModel>> GetReviewsByCompanyIdAsync(string companyId);
    Task<IEnumerable<ReviewViewModel>> GetReviewsByProductIdAsync(string productId);
    Task AddToProductAsync(ReviewAddViewModel reviewModel, string id, string userId);
    Task<bool> AlreadyReviewedProductAsync(string id, string userId);
    Task<bool> AlreadyReviewedCompanyAsync(string id, string userId);
    Task AddToCompanyAsync(ReviewAddViewModel reviewModel, string id, string userId);
}
