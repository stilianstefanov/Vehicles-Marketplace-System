namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ViewModels.Product;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly IImageService _imageService;

    public HomeController(
        IProductService productService,
        IImageService imageService)
    {
       _productService = productService;
       _imageService = imageService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetProductsForHomeAsync();

        products.ScooterProducts = products
            .ScooterProducts
            .Select(async scooterProduct =>
            {
                scooterProduct.Image = await _imageService.GetImageByIdAsync(scooterProduct.ImageId);
                return scooterProduct;
            })
            .Select(t => t.Result).ToList();

        products.BikeProducts = products
            .BikeProducts
            .Select(async bikeProduct =>
            {
                bikeProduct.Image = await _imageService.GetImageByIdAsync(bikeProduct.ImageId);
                return bikeProduct;
            })
            .Select(t => t.Result).ToList();

        products.AccessoryProducts = products
            .AccessoryProducts
            .Select(async accessoryProduct =>
            {
                accessoryProduct.Image = await _imageService.GetImageByIdAsync(accessoryProduct.ImageId);
                return accessoryProduct;
            })
            .Select(t => t.Result).ToList();

        return View(products);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int statusCode)
    {
        if (statusCode == 400 || statusCode == 404)
        {
            return this.View("Error404");
        }

        if (statusCode == 401)
        {
            return this.View("Error401");
        }

        return View();
    }
}
