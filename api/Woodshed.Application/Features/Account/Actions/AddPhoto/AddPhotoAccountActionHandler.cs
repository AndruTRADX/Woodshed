using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Photos;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Photos;

namespace Woodshed.Application.Features.Account.Actions.AddPhoto;

public class AddPhotoAccountActionHandler(IUserAccessor userAccessor, IUserAccountService accountService, IPhotoService photoService, IMapper mapper) : IRequestHandler<AddPhotoAccountAction, ApiResponse<PhotoResponse>>
{
    public async Task<ApiResponse<PhotoResponse>> Handle(AddPhotoAccountAction request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        var uploadResults = await photoService.UploadPhoto(request.Request.File)
            ?? throw new BadRequestException("Failed to upload photo");

        var photo = await accountService.AddPhotoAsync(userId, uploadResults.Url, uploadResults.PublicId, cancellationToken);

        var response = mapper.Map<PhotoResponse>(photo);
        return new ApiResponse<PhotoResponse>(response);
    }
}
