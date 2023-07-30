namespace ThinkElectric.Web.ViewModels.OrderItem;

public class OrderItemViewModel
{
    public string ProductName { get; set; } = null!;

    public int Quantity { get; set; }

    public string Price { get; set; } = null!;

    public string TotalSum { get; set; } = null!;
}
