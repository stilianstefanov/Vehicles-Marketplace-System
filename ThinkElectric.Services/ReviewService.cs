namespace ThinkElectric.Services;

using Contracts;
using Data;
using Data.Models;
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

    public async Task AddToProductAsync(ReviewAddViewModel reviewModel, string id, string userId)
    {
        Review review = new Review()
        {
            Content = reviewModel.Content,
            Rating = reviewModel.Rating,
            ProductId = Guid.Parse(id),
            UserId = Guid.Parse(userId),
            CreatedOn = DateTime.UtcNow
        };

        await _dbContext.Reviews.AddAsync(review);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> AlreadyReviewedProductAsync(string id, string userId)
    {
        bool alreadyReviewed = await _dbContext
            .Reviews
            .AnyAsync(r => r.ProductId.ToString() == id && r.UserId.ToString() == userId);

        return alreadyReviewed;
    }

    public async Task<bool> AlreadyReviewedCompanyAsync(string id, string userId)
    {
        bool alreadyReviewed = await _dbContext
            .Reviews
            .AnyAsync(r => r.CompanyId.ToString() == id && r.UserId.ToString() == userId);

        return alreadyReviewed;
    }

    public async Task AddToCompanyAsync(ReviewAddViewModel reviewModel, string id, string userId)
    {
        Review review = new Review()
        {
            Content = reviewModel.Content,
            Rating = reviewModel.Rating,
            CompanyId = Guid.Parse(id),
            UserId = Guid.Parse(userId),
            CreatedOn = DateTime.UtcNow
        };

        await _dbContext.Reviews.AddAsync(review);

        await _dbContext.SaveChangesAsync();
    }
}
