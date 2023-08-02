namespace ThinkElectric.Web.ViewModels.Order;

using Address;
using OrderItem;

public class OrderDetailsViewModel
{
    public string Id { get; set; } = null!;

    public string CreatedOn { get; set; } = null!;

    public string TotalSum { get; set; } = null!;

    public IEnumerable<OrderItemBuyerViewModel> OrderItems { get; set; } = null!;

    public AddressViewModel? Address { get; set; }
}
