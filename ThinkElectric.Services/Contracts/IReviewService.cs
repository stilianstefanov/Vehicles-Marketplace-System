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
    Task<IEnumerable<ReviewMineViewModel>> GetMineAsync(string userId);
    Task<bool> ReviewExistsAsync(string id);
    Task<bool> IsUserAuthorizedAsync(string id, string userId);
    Task DeleteAsync(string id);
    Task<ReviewEditViewModel> GetForEditAsync(string id);
    Task EditAsync(ReviewEditViewModel reviewModel, string id);
    Task DeleteAllReviewsByUserIdAsync(string userId);
}
