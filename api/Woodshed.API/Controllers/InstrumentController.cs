using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Woodshed.Application.Features.Instruments.Queries.GetAll;
using Woodshed.Application.Features.Instruments.Queries.GetById;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Instruments;

namespace Woodshed.API.Controllers;

[Authorize]
public class InstrumentController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<InstrumentResponse>>>> Get()
    {
        return await Mediator.Send(new GetInstrumentsQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<InstrumentResponse>>> GetById(string id)
    {
        return await Mediator.Send(new GetInstrumentByIdQuery() { Id = id });
    }
}
