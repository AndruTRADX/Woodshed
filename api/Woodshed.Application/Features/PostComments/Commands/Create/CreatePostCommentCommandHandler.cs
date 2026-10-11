using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.PostComments;
using Woodshed.Domain;

namespace Woodshed.Application.Features.PostComments.Commands.Create;

public class CreatePostCommentCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor, IMapper mapper) : IRequestHandler<CreatePostCommentCommand, ApiResponse<PostCommentResponse>>
{
    public async Task<ApiResponse<PostCommentResponse>> Handle(CreatePostCommentCommand request, CancellationToken cancellationToken)
    {
        var response = await unitOfWork.Repository<Post>().GetFirstAsync(predicate: x => x.Id == request.PostId, enableTracking: true)
            ?? throw new NotFoundException(nameof(Post), request.PostId);

        var userId = userAccessor.GetUserId();

        var data = mapper.Map<PostComment>(request.Request);

        response.AddComment(data, userId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var savedComment = await unitOfWork.Repository<PostComment>().GetFirstAsync(predicate: x => x.Id == data.Id, includeStrings: ["User"], enableTracking: false)
            ?? throw new NotFoundException(nameof(PostComment), data.Id);

        return new ApiResponse<PostCommentResponse>(mapper.Map<PostCommentResponse>(savedComment));
    }
}
