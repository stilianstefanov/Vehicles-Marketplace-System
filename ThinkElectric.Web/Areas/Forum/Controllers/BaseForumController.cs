namespace ThinkElectric.Web.Areas.Forum.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static Common.GeneralApplicationConstants;
using static Common.ErrorMessages;
using static Common.NotificationsMessagesConstants;

[Area(ForumAreaName)]
[Authorize]
public class BaseForumController : Controller
{
    protected IActionResult GeneralError()
    {
        this.TempData[ErrorMessage] = UnexpectedErrorMessage;

        return RedirectToAction("Index", "Home", new { Area = ForumAreaName });
    }
}
