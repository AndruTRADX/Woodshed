using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Posts;
using Woodshed.Domain;

namespace Woodshed.Application.Features.Posts.Queries.GetById;

public class GetPostByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : IRequestHandler<GetPostByIdQuery, ApiResponse<PostResponse>>
{
    public async Task<ApiResponse<PostResponse>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = userAccessor.GetUserId();

        var response = await unitOfWork.Repository<Post>()
            .GetFirstAsync<PostResponse>(
                predicate: x => x.Id == request.Id,
                configuration: mapper.ConfigurationProvider,
                cancellationToken: cancellationToken,
                new { currentUserId }
            ) ?? throw new NotFoundException(nameof(Post), request.Id);

        return new ApiResponse<PostResponse>(response);
    }
}
