using MediatR;
using Woodshed.Application.Models.Request.Identity;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.Identities.Commands.Register;

public class RegisterUserCommand : IRequest<ApiResponse<Guid>>
{
    public required RegisterUserRequest Account { get; set; }
}
