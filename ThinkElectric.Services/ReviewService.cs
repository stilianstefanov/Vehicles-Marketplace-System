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
                CreatedOn = r.CreatedOn.ToString("MM/dd/yyyy H:mm"),
                Rating = r.Rating,
                UserFullName = $"{r.User.FirstName} {r.User.LastName}"
            })
            .ToArrayAsync();

        return reviews;
    }

    public async Task<IEnumerable<ReviewViewModel>> GetReviewsByProductIdAsync(string productId)
    {
        IEnumerable<ReviewViewModel> reviews = await _dbContext
            .Reviews
            .Where(r => r.ProductId.ToString() == productId)
            .Select(r => new ReviewViewModel()
            {
                Content = r.Content,
                CreatedOn = r.CreatedOn.ToString("MM/dd/yyyy H:mm"),
                Rating = r.Rating,
                UserFullName = $"{r.User.FirstName} {r.User.LastName}"
            })
            .ToArrayAsync();

        return reviews;
    }
}
