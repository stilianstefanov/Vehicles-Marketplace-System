namespace ThinkElectric.Web.ViewModels.Post;

using Comment;

public class PostDetailsViewModel
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string CreatedOn { get; set; } = null!;

    public string UserFullName { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public int CommentsCount { get; set; }

    public IEnumerable<CommentViewModel> Comments { get; set; } = null!;

    public CommentAddViewModel CurrentComment { get; set; } = null!;
}
