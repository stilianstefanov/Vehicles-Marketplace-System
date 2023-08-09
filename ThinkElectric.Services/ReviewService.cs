namespace ThinkElectric.Services;

using Microsoft.EntityFrameworkCore;

using Contracts;
using Data;
using Data.Models;
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

    public async Task<IEnumerable<ReviewMineViewModel>> GetMineAsync(string userId)
    {
        IEnumerable<ReviewMineViewModel> reviews = await _dbContext
            .Reviews
            .Where(r => r.UserId.ToString() == userId)
            .Select(r => new ReviewMineViewModel()
            {
                Id = r.Id.ToString(),
                Content = r.Content,
                CreatedOn = r.CreatedOn.ToString("MM/dd/yyyy H:mm"),
                Rating = r.Rating,
                ProductId = string.IsNullOrEmpty(r.ProductId.ToString()) ? null : r.ProductId.ToString(),
                ProductName = string.IsNullOrEmpty(r.ProductId.ToString()) ? null : r.Product!.Name,
                CompanyId = string.IsNullOrEmpty(r.CompanyId.ToString()) ? null : r.CompanyId.ToString(),
                CompanyName = string.IsNullOrEmpty(r.CompanyId.ToString()) ? null : r.Company!.Name
            })
            .ToArrayAsync();

        return reviews;
    }

    public async Task<bool> ReviewExistsAsync(string id)
    {
        bool reviewExists = await _dbContext
            .Reviews
            .AnyAsync(r => r.Id.ToString() == id);

        return reviewExists;
    }

    public async Task<bool> IsUserAuthorizedAsync(string id, string userId)
    {
        bool isAuthorized = await _dbContext
            .Reviews
            .AnyAsync(r => r.Id.ToString() == id && r.UserId.ToString() == userId);

        return isAuthorized;
    }

    public async Task DeleteAsync(string id)
    {
        Review review = await _dbContext
            .Reviews
            .FirstAsync(r => r.Id.ToString() == id);

        _dbContext.Reviews.Remove(review);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<ReviewEditViewModel> GetForEditAsync(string id)
    {
        ReviewEditViewModel model = await _dbContext
            .Reviews
            .Where(r => r.Id.ToString() == id)
            .Select(r => new ReviewEditViewModel()
            {
                Content = r.Content,
                Rating = Convert.ToInt32(r.Rating)
            })
            .FirstAsync();

        return model;
    }

    public async Task EditAsync(ReviewEditViewModel reviewModel, string id)
    {
        Review review = await _dbContext
            .Reviews
            .FirstAsync(r => r.Id.ToString() == id);

        review.Content = reviewModel.Content;
        review.Rating = reviewModel.Rating;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAllReviewsByUserIdAsync(string userId)
    {
        IEnumerable<Review> reviews = await _dbContext            
            .Reviews
            .Where(r => r.UserId.ToString() == userId)
            .ToArrayAsync();

        _dbContext.Reviews.RemoveRange(reviews);

        await _dbContext.SaveChangesAsync();
    }
}
