namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Post;

public interface IPostService
{
    Task<IEnumerable<PostAllViewModel>> GetAllPostsAsync();
    Task CreateAsync(PostCreateViewModel postModel, string userId);
}
