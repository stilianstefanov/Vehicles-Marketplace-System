namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.CartItem;

public interface ICartService
{
    Task<string> CreateAsync(Guid userId);
    Task AddToCartAsync(string id, string userId);
    Task<bool> CartItemExistsAsync(string id);
    Task<bool> IsUserAuthorizedAsync(string id, string? userId);
    Task RemoveFromCartAsync(string id);
    Task<IList<CartItemViewModel>> GetAllAsync(string userId);
    Task<bool> ProductAlreadyAdded(string id, string userId);
    Task<bool> AreCartItemsValidAsync(IEnumerable<CartItemViewModel> cartItems);
    Task ClearAsync(string userId);
}
