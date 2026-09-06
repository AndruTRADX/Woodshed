using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Domain;

namespace Woodshed.Application.Features.PostLikes.Commands.Create;

public class CreatePostLikeCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor) : IRequestHandler<CreatePostLikeCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> Handle(CreatePostLikeCommand request, CancellationToken cancellationToken)
    {
        var response = await unitOfWork.Repository<Post>().GetFirstAsync(predicate: x => x.Id == request.PostId, enableTracking: true)
            ?? throw new NotFoundException(nameof(Post), request.PostId);

        var userId = userAccessor.GetUserIdOrDefault()
            ?? throw new UnauthorizedException();

        response.AddLike(userId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<string>(response.Id);
    }
}
