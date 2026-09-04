using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Domain;

namespace Woodshed.Application.Features.Posts.Commands.Delete;

public class DeletePostCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor) : IRequestHandler<DeletePostCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserIdOrDefault() ?? throw new UnauthorizedException();

        var data = await unitOfWork.Repository<Post>().GetFirstAsync(x => x.Id == request.Id)
            ?? throw new NotFoundException(nameof(Post), request.Id);

        data.Delete(userId);

        unitOfWork.Repository<Post>().DeleteEntity(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Unit>();
    }
}
