using MediatR;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.Account.Actions.FollowAccount;

public class FollowAccountAction : IRequest<ApiResponse<Unit>>
{
    public string TargetUserId { get; set; } = string.Empty;
}
