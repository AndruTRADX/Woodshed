using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Photos;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.Account.Actions.RemovePhoto;

public class RemovePhotoAccountActionHandler(IUserAccessor userAccessor, IUserAccountService accountService, IPhotoService photoService) : IRequestHandler<RemovePhotoAccountAction, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(RemovePhotoAccountAction request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        var photo = await accountService.RemovePhotoAsync(userId, request.PhotoId, cancellationToken);

        await photoService.DeletePhoto(photo.PublicId);

        return new ApiResponse<Unit>();
    }
}
