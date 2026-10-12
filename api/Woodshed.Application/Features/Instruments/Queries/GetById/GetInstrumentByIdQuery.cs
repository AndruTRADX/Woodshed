using MediatR;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Instruments;

namespace Woodshed.Application.Features.Instruments.Queries.GetById;

public class GetInstrumentByIdQuery : IRequest<ApiResponse<InstrumentResponse>>
{
    public required string Id { get; set; }
}
