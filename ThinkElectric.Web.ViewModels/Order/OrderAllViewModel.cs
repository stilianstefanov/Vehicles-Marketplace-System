using ThinkElectric.Web.ViewModels.OrderItem;

namespace ThinkElectric.Web.ViewModels.Order;

public class OrderAllViewModel
{
    public string CreatedOn { get; set; } = null!;

    public string TotalSum { get; set; } = null!;

    public string Status { get; set; } = null!;

    public IEnumerable<OrderItemViewModel> OrderItems { get; set; } = null!;
}
