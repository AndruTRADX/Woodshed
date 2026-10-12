using Microsoft.AspNetCore.Mvc;
using Woodshed.Application.Features.UserInstruments.Queries.GetByUserId;
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
}
