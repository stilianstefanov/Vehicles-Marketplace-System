namespace ThinkElectric.Web.ViewModels.Product;

public class ProductsHomeModel
{
    public ProductsHomeModel()
    {
        ScooterProducts = new HashSet<ProductViewModel>();
        AccessoryProducts = new HashSet<ProductViewModel>();
        BikeProducts = new HashSet<ProductViewModel>();
    }

    public IEnumerable<ProductViewModel> ScooterProducts { get; set; }

    public IEnumerable<ProductViewModel> AccessoryProducts { get; set; }

    public IEnumerable<ProductViewModel> BikeProducts { get; set; }
}
