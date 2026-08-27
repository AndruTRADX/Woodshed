using MediatR;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.Posts.Commands.Delete;

public class DeletePostCommand : IRequest<ApiResponse<Unit>>
{
    public required string Id { get; set; }
}
