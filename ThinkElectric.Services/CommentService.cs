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
}
