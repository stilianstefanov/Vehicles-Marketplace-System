namespace ThinkElectric.Web.ViewModels.Accessory;

using Product;

public class AccessoryDetailsViewModel
{
    public string Id { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string CompatibleBrand { get; set; } = null!;

    public string CompatibleModel { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public ProductDetailsViewModel Product { get; set; } = null!;
}
