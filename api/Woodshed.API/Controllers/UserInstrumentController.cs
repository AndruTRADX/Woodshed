using MediatR;
using Microsoft.AspNetCore.Mvc;
using Woodshed.Application.Features.UserInstruments.Commands.Create;
using Woodshed.Application.Features.UserInstruments.Commands.Delete;
using Woodshed.Application.Features.UserInstruments.Commands.Update;
using Woodshed.Application.Features.UserInstruments.Queries.GetByUserId;
using Woodshed.Application.Models.Request.UserInstruments;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.UserInstruments;

namespace Woodshed.API.Controllers;

public class UserInstrumentController : BaseApiController
{
    [HttpGet("{userId}")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<UserInstrumentResponse>>>> GetByUserId(string userId)
    {
        return await Mediator.Send(new GetUserInstrumentsQuery() { UserId = userId });
    }

    [HttpPost()]
    public async Task<ActionResult<ApiResponse<Unit>>> Create(CreateUserInstrumentRequest request)
    {
        return await Mediator.Send(new CreateUserInstrumentCommand() { Request = request });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<Unit>>> Update(string id, [FromBody] UpdateUserInstrumentRequest request)
    {
        return await Mediator.Send(new UpdateUserInstrumentCommand() { InstrumentId = id, Request = request });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<Unit>>> Delete(string id)
    {
        return await Mediator.Send(new DeleteUserInstrumentCommand() { InstrumentId = id});
    }
}
