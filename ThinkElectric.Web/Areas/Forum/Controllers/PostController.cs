namespace ThinkElectric.Web.Areas.Forum.Controllers;

using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ViewModels.Post;

using static Common.GeneralApplicationConstants;

public class PostController : BaseForumController
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(PostCreateViewModel postModel)
    {
        if (!ModelState.IsValid)
        {
            return View(postModel);
        }

        try
        {
            await _postService.CreateAsync(postModel, User.GetId()!);

            return RedirectToAction("Index", "Home", new { area = ForumAreaName });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }
}
