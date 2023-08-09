using Microsoft.AspNetCore.Mvc;

namespace ThinkElectric.Web.Areas.Admin.Controllers;

public class HomeController : BaseAdminController
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
