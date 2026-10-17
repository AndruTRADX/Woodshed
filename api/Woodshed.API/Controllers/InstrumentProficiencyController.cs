using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Woodshed.Application.Features.InstrumentProficiencies.Queries.GetAll;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.InstrumentProficiencies;

namespace Woodshed.API.Controllers;

[Authorize]
public class InstrumentProficiencyController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<InstrumentProficiencyResponse>>>> Get()
    {
        return await Mediator.Send(new GetInstrumentProficienciesQuery());
    }
}
