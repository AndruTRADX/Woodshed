using MediatR;
using Microsoft.AspNetCore.Mvc;
using Woodshed.Application.Features.Identities.Commands.Register;
using Woodshed.Application.Features.Identities.Commands.SignOut;
using Woodshed.Application.Features.Identities.Queries.GetAccount;
using Woodshed.Application.Models.Request.Identity;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Identity;

namespace Woodshed.API.Controllers;

public class AccountController : BaseApiController
{
    [HttpGet("my-account")]
    public async Task<ActionResult<ApiResponse<UserResponse?>>> GetAccount()
    {
        return await Mediator.Send(new GetAccountQuery());
    }

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
