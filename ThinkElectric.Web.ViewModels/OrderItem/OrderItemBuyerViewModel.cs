namespace ThinkElectric.Web.ViewModels.OrderItem;

public class OrderItemBuyerViewModel
{
    public string ProductName { get; set; } = null!;

    public int Quantity { get; set; }

    public string Price { get; set; } = null!;

    public string TotalSum { get; set; } = null!;

    public string Status { get; set; } = null!;
}
