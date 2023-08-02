namespace ThinkElectric.Web.ViewModels.OrderItem;

using Address;

public class OrderItemCompanyViewModel
{
    public string ProductName { get; set; } = null!;

    public int Quantity { get; set; }

    public string Price { get; set; } = null!;

    public string TotalSum { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string BuyerName { get; set; } = null!;

    public string BuyerPhoneNumber { get; set; } = null!;

    public AddressViewModel Address { get; set; } = null!;
}
