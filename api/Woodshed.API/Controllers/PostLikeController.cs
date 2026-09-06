using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Woodshed.Application.Features.PostLikes.Commands.Create;
using Woodshed.Application.Features.PostLikes.Commands.Delete;
using Woodshed.Application.Features.PostLikes.Queries.GetPaged;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.PostLikes;

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

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResponse<PostLikeResponse>>>> GetPaged([FromQuery] GetPagedPostLikeQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpDelete]
    public async Task<ActionResult<ApiResponse<Unit>>> Delete(string postId)
    {
        return await Mediator.Send(new DeletePostLikeCommand { PostId = postId });
    }
}
