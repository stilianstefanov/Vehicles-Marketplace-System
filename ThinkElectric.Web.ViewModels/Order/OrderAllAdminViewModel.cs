namespace ThinkElectric.Web.ViewModels.Order;

using OrderItem;

public class OrderAllAdminViewModel
{
    public string UserFullName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string CreatedOn { get; set; } = null!;

    public string TotalSum { get; set; } = null!;

    public string Status { get; set; } = null!;

    public IEnumerable<OrderItemBuyerViewModel> OrderItems { get; set; } = null!;
}
