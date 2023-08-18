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
        var post = new Post()
        {
            Title = postModel.Title,
            Content = postModel.Content,
            UserId = Guid.Parse(userId),
            CreatedOn = DateTime.UtcNow,
        };

        await _dbContext.Posts.AddAsync(post);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<PostDetailsViewModel?> GetDetailsAsync(string id)
    {
        var post = await _dbContext
            .Posts
            .Where(p => p.Id.ToString() == id)
            .Select(p => new PostDetailsViewModel()
            {
                Id = p.Id.ToString(),
                Title = p.Title,
                Content = p.Content,
                CreatedOn = p.CreatedOn.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                UserFullName = p.User.FirstName + " " + p.User.LastName,
                CommentsCount = p.Comments.Count(c => !c.IsDeleted),
                UserId = p.UserId.ToString()
            })
            .FirstOrDefaultAsync();

        return post;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        var postExists = await _dbContext
            .Posts
            .AnyAsync(p => p.Id.ToString() == id && !p.IsDeleted);

        return postExists;
    }

    public async Task DeleteAsync(string id)
    {
        var post = await _dbContext
            .Posts
            .FirstAsync(p => p.Id.ToString() == id);

        post.IsDeleted = true;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsUserAuthorizedAsync(string id, string userId)
    {
        var isUserAuthorized = await _dbContext
            .Posts
            .AnyAsync(p => p.Id.ToString() == id && p.UserId.ToString() == userId);

        return isUserAuthorized;
    }

    public async Task<PostEditViewModel> GetEditViewModelAsync(string id)
    {
        var post = await _dbContext          
            .Posts
            .Where(p => p.Id.ToString() == id)
            .Select(p => new PostEditViewModel()
            {
                Title = p.Title,
                Content = p.Content,
            })
            .FirstAsync();

        return post;
    }

    public async Task EditAsync(PostEditViewModel postModel, string id)
    {
        var post = await _dbContext           
            .Posts
            .FirstAsync(p => p.Id.ToString() == id);

        post.Title = postModel.Title;
        post.Content = postModel.Content;

        await _dbContext.SaveChangesAsync();
    }
}
