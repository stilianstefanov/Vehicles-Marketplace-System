namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.CartItem;
using Web.ViewModels.Order;
using Web.ViewModels.OrderItem;

public interface IOrderService
{
    Task<string> CreateAsync(IEnumerable<CartItemViewModel> cartItems, string userId);
    Task<OrderDetailsViewModel> GetOrderDetailsAsync(string id);
    Task<bool> OrderExistsAsync(string id);
    Task<bool> IsOrderFromUserAsync(string id, string userId);
    Task CancelAsync(string id);
    Task ConfirmAsync(string id);
    Task<bool> IsOrderExisting(string id);
    Task<IEnumerable<OrderAllViewModel>> GetAllByUserAsync(string userId);
    Task<IEnumerable<OrderItemCompanyViewModel>> GetAllByCompanyAsync(string companyId);
}
