using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Woodshed.Application.Contracts.Photos;
using Woodshed.Application.Models.Photos;
using Woodshed.Infrastructure.Models;

namespace Woodshed.Infrastructure.Services;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;
    private readonly CloudinarySettings _cloudinarySettings;

    public PhotoService(IOptions<CloudinarySettings> config)
    {
        _cloudinarySettings = config.Value;
        
        var account = new Account(
            _cloudinarySettings.CloudName,
            _cloudinarySettings.ApiKey,
            _cloudinarySettings.ApiSecret
        );

        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> DeletePhoto(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deleteParams);

        if (result.Error != null)
        {
            throw new ApplicationException(result.Error.Message);
        }

        return result.Result;
    }

    public async Task<PhotoUploadResults?> UploadPhoto(IFormFile file)
    {
        if (file.Length > 0)
        {
            await using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "Reactivities",
            };

            var uploadResults = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResults.Error != null)
            {
                throw new ApplicationException(uploadResults.Error.Message);
            }

            return new PhotoUploadResults
            {
                PublicId = uploadResults.PublicId,
                Url = uploadResults.SecureUrl.AbsoluteUri
            };
        }

        return null;
    }
}
