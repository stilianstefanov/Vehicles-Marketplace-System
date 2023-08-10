namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Post;

public interface IPostService
{
    Task<IEnumerable<PostAllViewModel>> GetAllPostsAsync();
    Task CreateAsync(PostCreateViewModel postModel, string userId);
    Task<PostDetailsViewModel?> GetDetailsAsync(string id);
    Task<bool> ExistsAsync(string id);
    Task DeleteAsync(string id);
    Task<bool> IsUserAuthorizedAsync(string id, string userId);
    Task<PostEditViewModel> GetEditViewModelAsync(string id);
    Task EditAsync(PostEditViewModel postModel, string id);
}
