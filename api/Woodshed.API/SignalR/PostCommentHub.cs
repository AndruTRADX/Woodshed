using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Woodshed.Application.Features.PostComments.Commands.Create;
using Woodshed.Application.Features.PostComments.Commands.Delete;
using Woodshed.Application.Features.PostComments.Queries.GetPaged;
using Woodshed.Application.Models.Request.PostComments;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.PostComments;

namespace Woodshed.API.SignalR;

public interface ICommentClient
{
    Task ReceiveComment(PostCommentResponse comment);
    Task CommentDeleted(string commentId);
}

public class PostCommentHub(IMediator mediator, IHttpContextAccessor httpContextAccessor) : Hub<ICommentClient>
{
    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var postId = httpContext!.Request.Query["postId"];

        if (string.IsNullOrEmpty(postId))
            throw new HubException("No post with this ID");

        await Groups.AddToGroupAsync(Context.ConnectionId, postId!);
        await base.OnConnectedAsync();
    }

    public async Task<PagedResponse<PostCommentResponse>> LoadComments(string postId, int pageIndex, int pageSize)
    {
        var query = new GetPagedPostCommentQuery { PostId = postId, PageIndex = pageIndex, PageSize = pageSize };
        return await SendAndUnwrap(query);
    }

    public async Task SendComment(string postId, string content)
    {
        var command = new CreatePostCommentCommand { PostId = postId, Request = new CreatePostCommentRequest { Content = content } };
        var comment = await SendAndUnwrap(command);
        await Clients.Group(postId).ReceiveComment(comment);
    }

    public async Task DeleteComment(string postId, string commentId)
    {
        var command = new DeletePostCommentCommand { PostId = postId, CommentId = commentId };
        await SendAndUnwrap(command);
        await Clients.Group(postId).CommentDeleted(commentId);
    }

    private async Task<T> SendAndUnwrap<T>(IRequest<ApiResponse<T>> request)
    {
        httpContextAccessor.HttpContext = Context.GetHttpContext();

        try
        {
            var response = await mediator.Send(request);
            return response.Data!;
        }
        catch (Exception ex) when (ex is ValidationException or ApplicationException)
        {
            throw new HubException(ex.Message);
        }
    }
}
