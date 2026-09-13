using MediatR;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.Account.Actions.SetMainPhoto;

public class SetMainPhotoAccountAction : IRequest<ApiResponse<Unit>>
{
    public required string PhotoId { get; set; }
}
