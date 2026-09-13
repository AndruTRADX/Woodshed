using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.Account.Actions.SetMainPhoto;

public class RemovePhotoAccountActionHandler(IUserAccessor userAccessor, IUserAccountService accountService) : IRequestHandler<SetMainPhotoAccountAction, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(SetMainPhotoAccountAction request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        await accountService.SetMainPhotoAsync(userId, request.PhotoId, cancellationToken);

        return new ApiResponse<Unit>();
    }
}
