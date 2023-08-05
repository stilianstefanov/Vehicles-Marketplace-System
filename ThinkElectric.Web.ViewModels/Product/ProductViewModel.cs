namespace ThinkElectric.Web.ViewModels.Product;

public class ProductViewModel
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Price { get; set; } = null!;

    public int Quantity { get; set; }

    public string CompanyId { get; set; } = null!;

    public string ImageId { get; set; } = null!;

    public ImageViewModel Image { get; set; } = null!;
}
