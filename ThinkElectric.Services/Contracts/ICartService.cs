namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Cart;

public interface ICartService
{
    Task<string> CreateAsync(Guid userId);
    Task AddToCartAsync(string id, string userId);
    Task<bool> CartItemExistsAsync(string id);
    Task<bool> IsUserAuthorizedAsync(string id, string? getId);
    Task RemoveFromCartAsync(string id);
    Task<IEnumerable<CartItemViewModel>> GetAllAsync(string getId);
}
