using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Domain;

namespace Woodshed.Application.Features.Posts.Commands.Create;

public class CreatePostCommandHandler(IUserAccessor userAccessor, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreatePostCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserIdOrDefault() ?? throw new UnauthorizedException();

        var data = mapper.Map<Post>(request.Request);
        data.UserId = userId;

        unitOfWork.Repository<Post>().AddEntity(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<string>(data.Id);
    }
}
