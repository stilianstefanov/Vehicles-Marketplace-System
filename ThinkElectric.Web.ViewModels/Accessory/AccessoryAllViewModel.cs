namespace ThinkElectric.Web.ViewModels.Accessory;

using Product;

public class AccessoryAllViewModel
{
    public string Id { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string CompatibleBrand { get; set; } = null!;

    public string CompatibleModel { get; set; } = null!;

    public ProductViewModel Product { get; set; } = null!;
}
