using MediatR;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.InstrumentProficiencies;

namespace Woodshed.Application.Features.InstrumentProficiencies.Queries.GetAll;

public class GetInstrumentProficienciesQuery : IRequest<ApiResponse<IReadOnlyList<InstrumentProficiencyResponse>>>
{

}
