namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Comment;
using Web.ViewModels.Post;

public interface ICommentService
{
    Task<IEnumerable<CommentViewModel>> GetAllCommentsAsync(string postId);
    Task CreateAsync(PostDetailsViewModel postModel, string userId);
    Task DeleteAllCommentsByPostIdAsync(string id);
}
