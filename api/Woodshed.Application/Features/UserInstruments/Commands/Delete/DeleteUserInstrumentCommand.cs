using System;
using MediatR;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.UserInstruments.Commands.Delete;

public class DeleteUserInstrumentCommand : IRequest<ApiResponse<Unit>>
{
    public required string InstrumentId { get; set; }
}
