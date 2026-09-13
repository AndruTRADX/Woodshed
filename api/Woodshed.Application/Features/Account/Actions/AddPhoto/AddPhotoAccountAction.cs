using MediatR;
using Woodshed.Application.Models.Request.Photos;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Photos;

namespace Woodshed.Application.Features.Account.Actions.AddPhoto;

public class AddPhotoAccountAction : IRequest<ApiResponse<PhotoResponse>>
{
    public required AddPhotoRequest Request { get; set; }
}
