using Microsoft.AspNetCore.Mvc;

namespace ThinkElectric.Web.Controllers;

public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
