using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Domain;

namespace Woodshed.Application.Features.PostComments.Commands.Create;

public class CreatePostCommentCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor, IMapper mapper) : IRequestHandler<CreatePostCommentCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> Handle(CreatePostCommentCommand request, CancellationToken cancellationToken)
    {
        var response = await unitOfWork.Repository<Post>().GetFirstAsync(
            predicate: x => x.Id == request.PostId, includeStrings: [], enabledTracking: true)
        ?? throw new NotFoundException(nameof(Post), request.PostId);

        var userId = userAccessor.GetUserIdOrDefault()
            ?? throw new UnauthorizedException();

        var data = mapper.Map<PostComment>(request.Request);

        response.AddComment(data, userId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<string>();
    }
}
