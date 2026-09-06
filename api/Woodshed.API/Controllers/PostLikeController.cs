using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Woodshed.Application.Features.PostLikes.Commands.Create;
using Woodshed.Application.Features.PostLikes.Commands.Delete;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.API.Controllers;

[Authorize]
[Route("api/post/{postId}/likes")]
public class PostLikeController : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<ApiResponse<string>>> Create(string postId)
    {
        return await Mediator.Send(new CreatePostLikeCommand { PostId = postId });
    }

    [HttpDelete]
    public async Task<ActionResult<ApiResponse<Unit>>> Delete(string postId)
    {
        return await Mediator.Send(new DeletePostLikeCommand { PostId = postId });
    }
}
