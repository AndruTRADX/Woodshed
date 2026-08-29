using MediatR;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Posts;
using Woodshed.Domain;

namespace Woodshed.Application.Features.Posts.Queries.GetById;

public class GetPostByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPostByIdQuery, ApiResponse<PostResponse>>
{
    public async Task<ApiResponse<PostResponse>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await unitOfWork.Repository<Post>()
            .GetFirstAsync(
                predicate: x => x.Id == request.Id,
                selector: x => new PostResponse
                {
                    Id = x.Id,
                    Content = x.Content,
                    CreatedAt = x.CreatedAt,
                    UserId = x.UserId,
                    CommentsCount = x.PostComments.Count,
                    LikesCount = x.PostLikes.Count
                }
            ) ?? throw new NotFoundException(nameof(Post), request.Id);

        return new ApiResponse<PostResponse>(response);
    }
}
