using MediatR;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.Account.Actions.Unfollow;

public class UnfollowAccountAction : IRequest<ApiResponse<Unit>>
{
    public string TargetUserId { get; set; } = string.Empty;
}
