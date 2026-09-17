using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.PostComments;
using Woodshed.Application.Specifications.PostComments;
using Woodshed.Domain;

namespace Woodshed.Application.Features.PostComments.Queries.GetPaged;

public class GetPagedPostCommentQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : IRequestHandler<GetPagedPostCommentQuery, ApiResponse<PagedResponse<PostCommentResponse>>>
{
    public async Task<ApiResponse<PagedResponse<PostCommentResponse>>> Handle(GetPagedPostCommentQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = userAccessor.GetUserId();

        var spec = new PostCommentSpecification(request, request.PostId);

        var data = await unitOfWork.Repository<PostComment>()
            .GetAllWithSpec<PostCommentResponse>(spec, mapper.ConfigurationProvider, cancellationToken, new { currentUserId });

        var specCount = new PostCommentCountSpecification(request, request.PostId);
        var totalCount = await unitOfWork.Repository<PostComment>().CountAsync(specCount);

        var totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount) / Convert.ToDecimal(request.PageSize)));

        return new ApiResponse<PagedResponse<PostCommentResponse>>(new()
        {
            Count = totalCount,
            Data = data,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
        });
    }
}
