using Microsoft.AspNetCore.Http;
using Woodshed.Application.Models.Videos;

namespace Woodshed.Application.Contracts.Videos;

public interface IVideoService
{
    Task<VideoUploadResults?> UploadVideo(IFormFile file, CancellationToken cancellationToken = default);
    Task<string> DeleteVideo(string publicId);
}