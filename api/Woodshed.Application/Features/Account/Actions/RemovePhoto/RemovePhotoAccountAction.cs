using MediatR;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.Account.Actions.RemovePhoto;

public class RemovePhotoAccountAction : IRequest<ApiResponse<Unit>>
{
    public required string PhotoId { get; set; }
}
