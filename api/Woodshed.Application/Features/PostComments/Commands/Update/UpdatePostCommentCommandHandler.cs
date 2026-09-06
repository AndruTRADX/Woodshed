using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Domain;

namespace Woodshed.Application.Features.PostComments.Commands.Update;

public class UpdatePostCommentCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor, IMapper mapper) : IRequestHandler<UpdatePostCommentCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> Handle(UpdatePostCommentCommand request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserIdOrDefault()
            ?? throw new UnauthorizedException();

        var response = await unitOfWork.Repository<PostComment>().GetFirstAsync(predicate: x => x.Id == request.CommentId && x.PostId == request.PostId)
            ?? throw new NotFoundException(nameof(PostComment), request.CommentId);

        response.PrepareUpdate(userId);

        var data = mapper.Map(request.Request, response);

        unitOfWork.Repository<PostComment>().UpdateEntity(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<string>(data.Id);
    }
}
