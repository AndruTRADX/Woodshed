using MediatR;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Domain;

namespace Woodshed.Application.Features.Posts.Commands.Delete;

public class DeletePostCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeletePostCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var data = await unitOfWork.Repository<Post>().GetFirstAsync(x => x.Id == request.Id)
            ?? throw new NotFoundException(nameof(Post), request.Id);

        unitOfWork.Repository<Post>().DeleteEntity(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Unit>();
    }
}
