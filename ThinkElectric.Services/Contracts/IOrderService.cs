namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.CartItem;
using Web.ViewModels.Order;

public interface IOrderService
{
    Task<string> CreateAsync(IEnumerable<CartItemViewModel> cartItems, string userId);
    Task<OrderDetailsViewModel> GetOrderDetailsAsync(string id);
    Task<bool> OrderExistsAsync(string id);
    Task<bool> IsOrderFromUserAsync(string id, string userId);
    Task CancelAsync(string id);
}
