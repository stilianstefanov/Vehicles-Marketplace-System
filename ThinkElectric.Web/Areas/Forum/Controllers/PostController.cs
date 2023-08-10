namespace ThinkElectric.Web.Areas.Forum.Controllers;

using Microsoft.AspNetCore.Mvc;

using Infrastructure.Extensions;
using Services.Contracts;
using ViewModels.Post;

using static Common.GeneralApplicationConstants;
using static Common.NotificationsMessagesConstants;
using static Common.ErrorMessages;
using static Common.GeneralMessages;

public class PostController : BaseForumController
{
    private readonly IPostService _postService;
    private readonly ICommentService _commentService;

    public PostController(IPostService postService, ICommentService commentService)
    {
        _postService = postService;
        _commentService = commentService;
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

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        try
        {
            var post = await _postService.GetDetailsAsync(id);

            if (post == null)
            {
                TempData[ErrorMessage] = PostNotFound;

                return RedirectToAction("Index", "Home", new { area = ForumAreaName });
            }

            post.Comments = await _commentService.GetAllCommentsAsync(id);

            return View(post);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        bool postExists = await _postService.ExistsAsync(id);

        if (!postExists)
        {
            TempData[ErrorMessage] = PostNotFound;

            return RedirectToAction("Index", "Home", new { area = ForumAreaName });
        }

        bool isUserAuthorized = await _postService.IsUserAuthorizedAsync(id, User.GetId()!);

        if (!isUserAuthorized && !User.IsAdmin())
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;

            return RedirectToAction("Index", "Home", new { area = ForumAreaName });
        }

        try
        {
            await _commentService.DeleteAllCommentsByPostIdAsync(id);

            await _postService.DeleteAsync(id);

            TempData[SuccessMessage] = PostDeleted;

            return RedirectToAction("Index", "Home", new { area = ForumAreaName });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        bool postExists = await _postService.ExistsAsync(id);

        if (!postExists)
        {
            TempData[ErrorMessage] = PostNotFound;

            return RedirectToAction("Index", "Home", new { area = ForumAreaName });
        }

        bool isUserAuthorized = await _postService.IsUserAuthorizedAsync(id, User.GetId()!);

        if (!isUserAuthorized && !User.IsAdmin())
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;

            return RedirectToAction("Index", "Home", new { area = ForumAreaName });
        }

        try
        {
            var post = await _postService.GetEditViewModelAsync(id);

            return View(post);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(PostEditViewModel postModel, string id)
    {
        if (!ModelState.IsValid)
        {
            return View(postModel);
        }

        bool postExists = await _postService.ExistsAsync(id);

        if (!postExists)
        {
            TempData[ErrorMessage] = PostNotFound;

            return RedirectToAction("Index", "Home", new { area = ForumAreaName });
        }

        bool isUserAuthorized = await _postService.IsUserAuthorizedAsync(id, User.GetId()!);

        if (!isUserAuthorized && !User.IsAdmin())
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;

            return RedirectToAction("Index", "Home", new { area = ForumAreaName });
        }

        try
        {
            await _postService.EditAsync(postModel, id);

            TempData[SuccessMessage] = PostEdited;

            return RedirectToAction("Details", "Post", new { area = ForumAreaName, id = id });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }
}
