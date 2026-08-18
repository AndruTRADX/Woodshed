namespace Woodshed.Application.Models.Response.Common;

public interface IApiResponse
{
    bool Success { get; }
    object? Data { get; }
}

public class ApiResponse<T> : IApiResponse
{
    public bool Success { get; set; } = false;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }

    object? IApiResponse.Data => Data;

    public ApiResponse()
    {
        Success = true;
        Title = "Success";
        Message = "Operation successful";
    }

    public ApiResponse(T data) : this()
    {
        Data = data;
    }

    public ApiResponse(T data, string message) : this()
    {
        Data = data;
        Message = message;
    }

    public ApiResponse(string title, string message, Dictionary<string, string[]> errors) : this()
    {
        Success = false;
        Title = title;
        Message = message ?? "An error has occurred";
        Errors = errors ?? [];
    }
}
