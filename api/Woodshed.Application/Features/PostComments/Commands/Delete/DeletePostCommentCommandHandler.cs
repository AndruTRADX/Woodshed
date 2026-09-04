using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Domain;

namespace Woodshed.Application.Features.PostComments.Commands.Delete;

public class DeletePostCommentCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor) : IRequestHandler<DeletePostCommentCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(DeletePostCommentCommand request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserIdOrDefault()
            ?? throw new UnauthorizedException();

        var comment = await unitOfWork.Repository<PostComment>().GetFirstAsync(
            predicate: x => x.Id == request.CommentId && x.PostId == request.PostId,
            includeStrings: [], enabledTracking: true)
            ?? throw new NotFoundException(nameof(PostComment), request.CommentId);

        comment.EnsureOwnedBy(userId);

        unitOfWork.Repository<PostComment>().DeleteEntity(comment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Unit>();
    }
}