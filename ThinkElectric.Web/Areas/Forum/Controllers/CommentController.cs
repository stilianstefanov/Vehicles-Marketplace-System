namespace ThinkElectric.Web.Areas.Forum.Controllers;

using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;
using ViewModels.Comment;
using ViewModels.Post;

using static Common.GeneralApplicationConstants;
using static Common.EntityValidationConstants.Comment;
using static Common.NotificationsMessagesConstants;
using static Common.ErrorMessages;
using static Common.GeneralMessages;

public class CommentController : BaseForumController
{
    private readonly ICommentService _commentService;
    private readonly IPostService _postService;

    public CommentController(ICommentService commentService, IPostService postService)
    {
        _commentService = commentService;
        _postService = postService;
    }

    [HttpPost]
    public async  Task<IActionResult> Add(PostDetailsViewModel postModel)
    {
        if (postModel.CurrentComment.Content.Length < ContentMinLength || 
            postModel.CurrentComment.Content.Length > ContentMaxLength)
        {
            return RedirectToAction("Details", "Post", new { area = ForumAreaName, id = postModel.Id });
        }

        bool postExists = await _postService.ExistsAsync(postModel.Id);

        if (!postExists)
        {
            return GeneralError();
        }

        try
        {
            await _commentService.CreateAsync(postModel, User.GetId()!);

            return RedirectToAction("Details", "Post", new { area = ForumAreaName, id = postModel.Id });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        bool commentExists = await _commentService.ExistsAsync(id);

        if (!commentExists)
        {
            return GeneralError();
        }

        bool isUserAuthorized = await _commentService.IsUserAuthorizedAsync(id, User.GetId()!);

        if (!isUserAuthorized && !User.IsAdmin())
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;

            return RedirectToAction("Index", "Home", new { area = ForumAreaName });
        }

        try
        {
            var postId = await _commentService.DeleteAsync(id);

            TempData[SuccessMessage] = CommentDeleted;

            return RedirectToAction("Details", "Post", new { area = ForumAreaName, id = postId });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        bool commentExists = await _commentService.ExistsAsync(id);

        if (!commentExists)
        {
            return GeneralError();
        }

        bool isUserAuthorized = await _commentService.IsUserAuthorizedAsync(id, User.GetId()!);

        if (!isUserAuthorized && !User.IsAdmin())
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;

            return RedirectToAction("Index", "Home", new { area = ForumAreaName });
        }

        try
        {
            var comment = await _commentService.GetCommentForEditAsync(id);

            return View(comment);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CommentEditViewModel commentModel, string id)
    {
        if (!ModelState.IsValid)
        {
            return View(commentModel);
        }

        bool commentExists = await _commentService.ExistsAsync(id);

        if (!commentExists)
        {
            return GeneralError();
        }

        bool isUserAuthorized = await _commentService.IsUserAuthorizedAsync(id, User.GetId()!);

        if (!isUserAuthorized && !User.IsAdmin())
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;

            return RedirectToAction("Index", "Home", new { area = ForumAreaName });
        }

        try
        {
            var postId = await _commentService.EditAsync(commentModel, id);

            TempData[SuccessMessage] = CommentEdited;

            return RedirectToAction("Details", "Post", new { area = ForumAreaName, id = postId });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }
}
