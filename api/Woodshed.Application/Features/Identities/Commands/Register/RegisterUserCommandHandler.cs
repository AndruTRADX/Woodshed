using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.Identities.Commands.Register;

public class RegisterUserCommandHandler(IAuthService authService) : IRequestHandler<RegisterUserCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var response = await authService.RegisterUserAsync(request.Account);

        return new ApiResponse<Guid>(response.UserId);
    }
}
