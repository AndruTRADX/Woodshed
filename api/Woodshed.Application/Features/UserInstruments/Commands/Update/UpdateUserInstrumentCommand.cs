using MediatR;
using Woodshed.Application.Models.Request.UserInstruments;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.UserInstruments.Commands.Update;

public class UpdateUserInstrumentCommand : IRequest<ApiResponse<Unit>>
{
    public required string InstrumentId { get; set; }
    public required UpdateUserInstrumentRequest Request { get; set; }
}
