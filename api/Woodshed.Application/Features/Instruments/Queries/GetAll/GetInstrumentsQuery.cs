using MediatR;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Instruments;

namespace Woodshed.Application.Features.Instruments.Queries.GetAll;

public class GetInstrumentsQuery : IRequest<ApiResponse<IReadOnlyList<InstrumentResponse>>>
{
}
