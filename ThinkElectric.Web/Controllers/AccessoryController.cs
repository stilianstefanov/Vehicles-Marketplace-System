namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

[Authorize]
public class AccessoryController : Controller
{
    private readonly IAccessoryService _accessoryService;
    private readonly IProductService _productService;
    private readonly IImageService _imageService;

    public AccessoryController(IAccessoryService accessoryService, IProductService productService, IImageService imageService)
    {
        _productService = productService;
        _imageService = imageService;
        _accessoryService = accessoryService;
    }

    [HttpGet]
    [Authorize(Policy = "CompanyOnly")]
    public IActionResult Create()
    {
        return View();
    }
}
