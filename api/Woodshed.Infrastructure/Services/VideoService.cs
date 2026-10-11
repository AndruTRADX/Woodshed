using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Woodshed.Application.Contracts.Videos;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Videos;
using Woodshed.Infrastructure.Models;

namespace Woodshed.Infrastructure.Services;

public class VideoService : IVideoService
{
    private const long MaxVideoBytes = 100 * 1024 * 1024;
    private const int ChunkSize = 10 * 1024 * 1024;

    private static readonly HashSet<string> AllowedContentTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "video/mp4", "video/quicktime", "video/webm", "video/x-matroska"
    };

    private readonly Cloudinary _cloudinary;

    public VideoService(IOptions<CloudinarySettings> config)
    {
        var settings = config.Value;
        _cloudinary = new Cloudinary(new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret));
    }

    public async Task<VideoUploadResults?> UploadVideo(IFormFile file, CancellationToken cancellationToken = default)
    {
        if (file.Length == 0)
        {
            return null;
        }

        if (file.Length > MaxVideoBytes)
        {
            throw new BadRequestException($"Video exceeds the {MaxVideoBytes / 1024 / 1024} MB limit");
        }

        if (!AllowedContentTypes.Contains(file.ContentType))
        {
            throw new BadRequestException("Unsupported video format");
        }

        await using var stream = file.OpenReadStream();

        var uploadParams = new VideoUploadParams
        {
            File = new FileDescription($"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}", stream),
            Folder = "Woodshed/videos",
        };

        var result = await _cloudinary.UploadLargeAsync<VideoUploadResult>(uploadParams, ChunkSize, cancellationToken);

        if (result.Error != null)
        {
            throw new ApplicationException(result.Error.Message);
        }

        var url = result.SecureUrl.AbsoluteUri;

        return new VideoUploadResults
        {
            PublicId = result.PublicId,
            Url = url,
            ThumbnailUrl = Path.ChangeExtension(url, ".jpg"),
            DurationSeconds = result.Duration
        };
    }

    public async Task<string> DeleteVideo(string publicId)
    {
        var deleteParams = new DeletionParams(publicId)
        {
            ResourceType = ResourceType.Video
        };

        var result = await _cloudinary.DestroyAsync(deleteParams);

        if (result.Error != null)
        {
            throw new ApplicationException(result.Error.Message);
        }

        return result.Result;
    }
}