namespace ThinkElectric.Web.Areas.Forum.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

public class HomeController : BaseForumController
{
    private readonly IPostService _postService;

    public HomeController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var posts = await _postService.GetAllPostsAsync();

            return View(posts);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }
}
