using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Domain;

namespace Woodshed.Application.Features.PostLikes.Commands.Delete;

public class DeletePostLikeCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor) : IRequestHandler<DeletePostLikeCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(DeletePostLikeCommand request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserIdOrDefault()
            ?? throw new UnauthorizedException();

        var like = await unitOfWork.Repository<PostLike>().GetFirstAsync(
            predicate: x => x.UserId == userId && x.PostId == request.PostId,
            enableTracking: true
        )
            ?? throw new NotFoundException(nameof(PostLike), request.PostId);

        like.EnsureOwnedBy(userId);

        unitOfWork.Repository<PostLike>().DeleteEntity(like);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Unit>();
    }
}
