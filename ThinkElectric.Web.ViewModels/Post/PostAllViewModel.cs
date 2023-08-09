namespace ThinkElectric.Web.ViewModels.Post;

public class PostAllViewModel
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string CreatedOn { get; set; } = null!;

    public string UserFullName { get; set; } = null!;

    public int CommentsCount { get; set; }
}
