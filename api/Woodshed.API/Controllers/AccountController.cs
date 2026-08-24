using MediatR;
using Microsoft.AspNetCore.Mvc;
using Woodshed.Application.Features.Identities.Commands.Register;
using Woodshed.Application.Features.Identities.Commands.SignOut;
using Woodshed.Application.Models.Request;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.API.Controllers;

public class AccountController : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<Guid>>> RegisterUser(RegisterUserRequest account)
    {
        return await Mediator.Send(new RegisterUserCommand { Account = account });
    }

    [HttpPost("sign-out")]
    public new async Task<ActionResult<ApiResponse<Unit>>> SignOut()
    {
        return await Mediator.Send(new SignOutUserCommand());
    }
}
