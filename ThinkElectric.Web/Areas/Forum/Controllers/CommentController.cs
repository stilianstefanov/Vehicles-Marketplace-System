namespace ThinkElectric.Web.Areas.Forum.Controllers;

using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;
using ViewModels.Post;

using static Common.GeneralApplicationConstants;
using static Common.EntityValidationConstants.Comment;

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
}
