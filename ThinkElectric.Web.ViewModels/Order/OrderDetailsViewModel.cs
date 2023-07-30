namespace ThinkElectric.Web.ViewModels.Order;

using OrderItem;

public class OrderDetailsViewModel
{
    public string Id { get; set; } = null!;

    public string CreatedOn { get; set; } = null!;

    public string TotalSum { get; set; } = null!;

    public IEnumerable<OrderItemViewModel> OrderItems { get; set; } = null!;
}
