using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Woodshed.Application.Features.Posts.Commands.Create;
using Woodshed.Application.Features.Posts.Commands.Delete;
using Woodshed.Application.Models.Request.Posts;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.API.Controllers;

[Authorize]
public class PostController : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<ApiResponse<string>>> Create(CreatePostRequest request)
    {
        return await Mediator.Send(new CreatePostCommand() { Request = request });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<Unit>>> Delete(string id)
    {
        return await Mediator.Send(new DeletePostCommand() { Id = id });
    }
}
