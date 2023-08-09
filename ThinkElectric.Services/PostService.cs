namespace ThinkElectric.Services;

using System.Globalization;
using Microsoft.EntityFrameworkCore;

using Contracts;
using Data;
using Data.Models;
using Web.ViewModels.Post;

public class PostService : IPostService
{
    private readonly ThinkElectricDbContext _dbContext;

    public PostService(ThinkElectricDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<PostAllViewModel>> GetAllPostsAsync()
    {
        IEnumerable<PostAllViewModel> posts = await _dbContext
            .Posts
            .Where(p => !p.IsDeleted)
            .OrderByDescending(p => p.CreatedOn)
            .Select(p => new PostAllViewModel()
            {
                Id = p.Id.ToString(),
                Title = p.Title,
                Content = p.Content,
                CreatedOn = p.CreatedOn.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                UserFullName = p.User.FirstName + " " + p.User.LastName,
                CommentsCount = p.Comments.Count(c => !c.IsDeleted),
                UserId = p.UserId.ToString(),
            })
            .ToArrayAsync();

        return posts;
    }

    public async Task CreateAsync(PostCreateViewModel postModel, string userId)
    {
        Post post = new Post()
        {
            Title = postModel.Title,
            Content = postModel.Content,
            UserId = Guid.Parse(userId),
            CreatedOn = DateTime.UtcNow,
        };

        await _dbContext.Posts.AddAsync(post);

        await _dbContext.SaveChangesAsync();
    }
}
