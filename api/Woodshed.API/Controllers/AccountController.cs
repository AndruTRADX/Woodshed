using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Woodshed.Application.Features.Account.Actions.AddPhoto;
using Woodshed.Application.Features.Account.Actions.EditAccount;
using Woodshed.Application.Features.Account.Actions.FollowAccount;
using Woodshed.Application.Features.Account.Actions.RemovePhoto;
using Woodshed.Application.Features.Account.Actions.SetMainPhoto;
using Woodshed.Application.Features.Account.Actions.Unfollow;
using Woodshed.Application.Features.Account.Queries.GetById;
using Woodshed.Application.Features.Account.Queries.GetPagedFollowers;
using Woodshed.Application.Features.Account.Queries.GetPagedFollowing;
using Woodshed.Application.Features.Account.Queries.GetPagePhotos;
using Woodshed.Application.Models.Request.Identity;
using Woodshed.Application.Models.Request.Photos;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Follow;
using Woodshed.Application.Models.Response.Identity;
using Woodshed.Application.Models.Response.Photos;

namespace Woodshed.API.Controllers;

[Authorize]
public class AccountController : BaseApiController
{
    [HttpGet("{userId}")]
    public async Task<ActionResult<ApiResponse<UserAccountResponse>>> GetById(string userId)
    {
        return await Mediator.Send(new GetAccountByIdQuery { UserId = userId });
    }

    [HttpPut]
    public async Task<ActionResult<ApiResponse<UserAccountResponse>>> EditAccount([FromBody] EditAccountRequest request)
    {
        return await Mediator.Send(new EditAccountAction { Request = request });
    }

    [HttpPost("photos")]
    public async Task<ActionResult<ApiResponse<PhotoResponse>>> AddPhoto([FromForm] AddPhotoRequest request)
    {
        return await Mediator.Send(new AddPhotoAccountAction { Request = request });
    }

    [HttpDelete("photos/{photoId}")]
    public async Task<ActionResult<ApiResponse<Unit>>> RemovePhoto(string photoId)
    {
        return await Mediator.Send(new RemovePhotoAccountAction { PhotoId = photoId });
    }

    [HttpPut("photos/{photoId}/main")]
    public async Task<ActionResult<ApiResponse<Unit>>> SetMainPhoto(string photoId)
    {
        return await Mediator.Send(new SetMainPhotoAccountAction { PhotoId = photoId });
    }

    [HttpGet("{userId}/photos")]
    public async Task<ActionResult<ApiResponse<PagedResponse<PhotoResponse>>>> GetPagedPhotos(string userId, [FromQuery] GetPagedAccountPhotosQuery query)
    {
        query.UserId = userId;
        return await Mediator.Send(query);
    }

    [HttpPost("{userId}/follow")]
    public async Task<ActionResult<ApiResponse<Unit>>> Follow(string userId)
    {
        return await Mediator.Send(new FollowAccountAction { TargetUserId = userId });
    }

    [HttpDelete("{userId}/follow")]
    public async Task<ActionResult<ApiResponse<Unit>>> Unfollow(string userId)
    {
        return await Mediator.Send(new UnfollowAccountAction { TargetUserId = userId });
    }

    [HttpGet("{userId}/followers")]
    public async Task<ActionResult<ApiResponse<PagedResponse<FollowerResponse>>>> GetPagedFollowers(string userId, [FromQuery] GetPagedFollowersQuery query)
    {
        query.UserId = userId;
        return await Mediator.Send(query);
    }

    [HttpGet("{userId}/following")]
    public async Task<ActionResult<ApiResponse<PagedResponse<FolloweeResponse>>>> GetPagedFollowing(string userId, [FromQuery] GetPagedFollowingQuery query)
    {
        query.UserId = userId;
        return await Mediator.Send(query);
    }
}
