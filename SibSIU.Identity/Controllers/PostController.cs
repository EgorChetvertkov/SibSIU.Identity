using Microsoft.AspNetCore.Mvc;
using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.ResultObject.Extensions;
using SibSIU.Domain.UserManager.Posts.Commands.Create;
using SibSIU.Domain.UserManager.Posts.Commands.Delete;
using SibSIU.Domain.UserManager.Posts.Commands.Update;
using SibSIU.Domain.UserManager.Posts.Queries.GetDetails;
using SibSIU.Domain.UserManager.Posts.Queries.GetPage;
using SibSIU.Domain.UserManager.Posts.Queries.GetSelectList;
using SibSIU.Identity.Models.Posts;
using System.Net.Mime;

namespace SibSIU.Identity.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PostController(
    ICreatePostHandler create,
    IUpdatePostHandler update,
    IDeletePostHandler delete,
    IGetPostDetailsHandler getDetails,
    IGetPostSelectListHandler getSelectList,
    IGetPostPageHandler getPage) : ControllerBase
{
    [HttpPost("create")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(PostDetails post, CancellationToken cancellationToken)
    {
        var result = await create.Handle(new(post.Id, post.Name), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPut("update")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(PostDetails post, CancellationToken cancellationToken)
    {
        var result = await update.Handle(new(post.Id, post.Name), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpDelete("delete")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(Ulid postId, CancellationToken cancellationToken)
    {
        var result = await delete.Handle(new(postId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpGet("{postId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(PostDetails), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDetails(Ulid postId, CancellationToken cancellationToken)
    {
        var result = await getDetails.Handle(new(postId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpGet("list")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(List<PostItem>), StatusCodes.Status200OK)]
    public async Task<List<PostItem>> GetDetails(string filter, CancellationToken cancellationToken)
    {
        return await getSelectList.Handle(new(filter), cancellationToken);
    }

    [HttpGet()]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(PaginationList<PostRowItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPage([FromQuery] GetPostPageRequest request, CancellationToken cancellationToken)
    {
        var result = await getPage.Handle(request, cancellationToken);
        return result.MapToActionResult();
    }
}
