namespace ThinkElectric.Services;

using System.Globalization;
using Microsoft.EntityFrameworkCore;

using Contracts;
using Data;
using Data.Models;
using Web.ViewModels.Comment;
using Web.ViewModels.Post;

public class CommentService : ICommentService
{
    private readonly ThinkElectricDbContext _dbContext;

    public CommentService(ThinkElectricDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CommentViewModel>> GetAllCommentsAsync(string postId)
    {
        IEnumerable<CommentViewModel> comments = await _dbContext
            .Comments
            .Where(c => c.PostId.ToString() == postId && !c.IsDeleted)
            .OrderByDescending(c => c.CreatedOn)
            .Select(c => new CommentViewModel()
            {
                Id = c.Id.ToString(),
                Content = c.Content,
                CreatedOn = c.CreatedOn.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                UserFullName = c.User.FirstName + " " + c.User.LastName,
                UserId = c.UserId.ToString(),
            })
            .ToArrayAsync();

        return comments;
    }

    public async Task CreateAsync(PostDetailsViewModel postModel, string userId)
    {
        Comment comment = new Comment()
        {
            Content = postModel.CurrentComment.Content,
            UserId = Guid.Parse(userId),
            PostId = Guid.Parse(postModel.Id),
            CreatedOn = DateTime.UtcNow,
        };

        await _dbContext.Comments.AddAsync(comment);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAllCommentsByPostIdAsync(string id)
    {
        IEnumerable<Comment> comments = await _dbContext        
            .Comments
            .Where(c => c.PostId.ToString() == id)
            .ToArrayAsync();

        foreach (Comment comment in comments)
        {
            comment.IsDeleted = true;
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string id)
    {
        bool commentExists = await _dbContext
            .Comments
            .AnyAsync(c => c.Id.ToString() == id && !c.IsDeleted);

        return commentExists;
    }

    public async Task<bool> IsUserAuthorizedAsync(string id, string userId)
    {
        bool isUserAuthorized = await _dbContext
            .Comments
            .AnyAsync(c => c.Id.ToString() == id && c.UserId.ToString() == userId);

        return isUserAuthorized;
    }

    public async Task<string> DeleteAsync(string id)
    {
        Comment comment = await _dbContext
            .Comments
            .FirstAsync(c => c.Id.ToString() == id);

        comment.IsDeleted = true;

        await _dbContext.SaveChangesAsync();

        return comment.PostId.ToString();
    }
}
