namespace ThinkElectric.Web.ViewModels.OrderItem;

public class OrderItemExcelModel
{
    public string ProductName { get; set; } = null!;

    public int Quantity { get; set; }

    public string Price { get; set; } = null!;

    public string TotalSum { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string BuyerName { get; set; } = null!;

    public string BuyerPhoneNumber { get; set; } = null!;

    public string CreatedOn { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string ZipCode { get; set; } = null!;
}
