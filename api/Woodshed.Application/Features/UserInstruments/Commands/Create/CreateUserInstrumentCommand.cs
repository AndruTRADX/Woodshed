using MediatR;
using Woodshed.Application.Models.Request.UserInstruments;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.UserInstruments.Commands.Create;

public class CreateUserInstrumentCommand : IRequest<ApiResponse<Unit>>
{
    public required CreateUserInstrumentRequest Request { get; set; }
}
