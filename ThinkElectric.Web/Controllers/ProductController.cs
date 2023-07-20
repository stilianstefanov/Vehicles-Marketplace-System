namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ViewModels.Product;

[Authorize]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly IImageService _imageService;

    public ProductController(IProductService productService, IImageService imageService)
    {
        _productService = productService;
        _imageService = imageService;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult AllByCompanyId()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> AllByCompanyIdFilteredAndPaged(ProductAllQueryModel queryModel)
    {
        queryModel = await _productService.AllByCompanyIdAsync(queryModel);

        queryModel.Products = queryModel
            .Products
            .Select(async product =>
            {
                product.Image = await _imageService.GetImageByIdAsync(product.ImageId);
                return product;
            })
            .Select(t => t.Result).ToList();

        return Json(queryModel);
    }
}
