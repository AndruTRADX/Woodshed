
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Woodshed.Application.Features.PostComments.Commands.Create;
using Woodshed.Application.Features.PostComments.Commands.Delete;
using Woodshed.Application.Features.PostComments.Commands.Update;
using Woodshed.Application.Features.PostComments.Queries.GetPaged;
using Woodshed.Application.Models.Request.PostComments;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.PostComments;

namespace Woodshed.API.Controllers;

[Authorize]
[Route("api/post/{postId}/comments")]
public class PostCommentController : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<ApiResponse<string>>> Create(string postId, [FromBody] CreatePostCommentRequest request)
    {
        return await Mediator.Send(new CreatePostCommentCommand { PostId = postId, Request = request });
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResponse<PostCommentResponse>>>> GetPaged([FromQuery] GetPagedPostCommentQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPut("{commentId}")]
    public async Task<ActionResult<ApiResponse<string>>> Update(string postId, string commentId, [FromBody] UpdatePostCommentRequest request)
    {
        return await Mediator.Send(new UpdatePostCommentCommand { PostId = postId, CommentId = commentId, Request = request });
    }

    [HttpDelete("{commentId}")]
    public async Task<ActionResult<ApiResponse<Unit>>> Delete(string postId, string commentId)
    {
        return await Mediator.Send(new DeletePostCommentCommand { PostId = postId, CommentId = commentId });
    }
}
