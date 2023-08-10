namespace ThinkElectric.Web.ViewModels.Comment;

public class CommentViewModel
{
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string CreatedOn { get; set; } = null!;

    public string UserFullName { get; set; } = null!;

    public string UserId { get; set; } = null!;
}
