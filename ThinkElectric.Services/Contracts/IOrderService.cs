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
    Task<bool> OrderItemExistsAsync(string orderItemId);
    Task<bool> IsOrderItemFromCompanyAsync(string orderItemId, string companyId);
    Task MarkAsFulfilledAsync(string orderItemId);
    Task<IEnumerable<OrderAllAdminViewModel>> GetAllOrdersAsync();
    Task<byte[]> GetAllInExcelFormatAsync(IEnumerable<OrderItemExcelModel> orderItemModels);
    Task<IEnumerable<OrderItemExcelModel>> GetOrderItemsForExcelAsync(string companyId);
    Task<bool> HasOrdersAsync(string companyId);
}
