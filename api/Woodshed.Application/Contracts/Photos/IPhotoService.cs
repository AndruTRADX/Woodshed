using Microsoft.AspNetCore.Http;
using Woodshed.Application.Models.Photos;

namespace Woodshed.Application.Contracts.Photos;

public interface IPhotoService
{
    Task<PhotoUploadResults?> UploadPhoto(IFormFile file);
    Task<string> DeletePhoto(string publicId);
}
