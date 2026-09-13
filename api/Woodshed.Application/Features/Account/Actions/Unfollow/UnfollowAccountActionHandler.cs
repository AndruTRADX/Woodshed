using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.Account.Actions.Unfollow;

public class UnfollowAccountActionHandler(IUserAccessor userAccessor, IUserAccountService userAccountService) : IRequestHandler<UnfollowAccountAction, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(UnfollowAccountAction request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        await userAccountService.UnfollowAsync(userId, request.TargetUserId, cancellationToken);

        return new ApiResponse<Unit>();
    }
}