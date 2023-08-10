namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Comment;
using Web.ViewModels.Post;

public interface ICommentService
{
    Task<IEnumerable<CommentViewModel>> GetAllCommentsAsync(string postId);
    Task CreateAsync(PostDetailsViewModel postModel, string userId);
    Task DeleteAllCommentsByPostIdAsync(string id);
    Task<bool> ExistsAsync(string id);
    Task<bool> IsUserAuthorizedAsync(string id, string userId);
    Task<string> DeleteAsync(string id);
    Task<CommentEditViewModel> GetCommentForEditAsync(string id);
    Task<string> EditAsync(CommentEditViewModel commentModel, string id);
}
