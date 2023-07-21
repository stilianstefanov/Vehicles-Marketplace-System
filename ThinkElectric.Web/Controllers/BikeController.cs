namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using ThinkElectric.Services.Contracts;

public class BikeController : Controller
{
    private readonly IScooterService _scooterService;
    private readonly IProductService _productService;
    private readonly IImageService _imageService;

    public BikeController(IScooterService scooterService, IProductService productService, IImageService imageService)
    {
        _scooterService = scooterService;
        _productService = productService;
        _imageService = imageService;
    }

    public IActionResult Index()
    {
        return View();
    }
}
