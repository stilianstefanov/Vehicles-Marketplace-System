namespace ThinkElectric.Services;

using Contracts;
using Data;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Review;

public class ReviewService : IReviewService
{
    private readonly ThinkElectricDbContext _dbContext;

    public ReviewService(ThinkElectricDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<ReviewViewModel>> GetReviewsByCompanyIdAsync(string companyId)
    {
        IEnumerable<ReviewViewModel> reviews = await _dbContext
            .Reviews
            .Where(r => r.CompanyId.ToString() == companyId)
            .Select(r => new ReviewViewModel()
            {
                Content = r.Content,
                CreatedOn = r.CreatedOn.ToString("dd/MM/yyyy"),
                ModifiedOn = r.ModifiedOn.HasValue ? r.ModifiedOn.Value.ToString("dd/MM/yyyy") : null,
                Rating = r.Rating
            })
            .ToArrayAsync();

        return reviews;
    }
}
