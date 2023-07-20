namespace ThinkElectric.Web.ViewModels.Product;

public class ProductAllViewModel
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Price { get; set; } = null!;

    public int Quantity { get; set; }

    public string CreatedOn { get; set; } = null!;

    public string ProductType { get; set; } = null!;

    public string ImageId { get; set; } = null!;

    public ImageViewModel Image { get; set; } = null!;
}
