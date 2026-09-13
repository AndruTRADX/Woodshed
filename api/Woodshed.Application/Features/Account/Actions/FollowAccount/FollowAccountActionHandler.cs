using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.Account.Actions.FollowAccount;

public class FollowAccountActionHandler(IUserAccessor userAccessor, IUserAccountService accountService) : IRequestHandler<FollowAccountAction, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(FollowAccountAction request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        await accountService.FollowAsync(userId, request.TargetUserId, cancellationToken);

        return new ApiResponse<Unit>();
    }
}
