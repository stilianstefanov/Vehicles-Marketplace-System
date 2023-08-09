namespace ThinkElectric.Web.Areas.Forum.Controllers;

using Microsoft.AspNetCore.Mvc;

public class HomeController : BaseForumController
{
    public IActionResult Index()
    {
        return Ok();
    }
}
