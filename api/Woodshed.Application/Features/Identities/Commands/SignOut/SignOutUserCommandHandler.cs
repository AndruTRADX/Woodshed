using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.Identities.Commands.SignOut;

public class SignOutUserCommandHandler(IAuthService authService) : IRequestHandler<SignOutUserCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(SignOutUserCommand request, CancellationToken cancellationToken)
    {
        await authService.SignOutAsync();

        return new ApiResponse<Unit>();
    }
}