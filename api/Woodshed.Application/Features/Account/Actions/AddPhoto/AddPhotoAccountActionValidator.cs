using FluentValidation;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace Woodshed.Application.Features.Account.Actions.AddPhoto;

public class AddPhotoAccountActionValidator : AbstractValidator<AddPhotoAccountAction>
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];
    private static readonly string[] AllowedContentTypes = ["image/jpeg", "image/png", "image/webp"];
    private const long MaxFileSizeBytes = 20 * 1024 * 1024;
    private const int MaxDimensionPx = 8000;

    public AddPhotoAccountActionValidator()
    {
        RuleFor(x => x.Request.File)
            .NotNull()
            .WithMessage("A file is required.");

        When(x => x.Request.File is not null, () =>
        {
            RuleFor(x => x.Request.File.Length)
                .GreaterThan(0).WithMessage("File is empty.")
                .LessThanOrEqualTo(MaxFileSizeBytes)
                .WithMessage($"File size must not be over {MaxFileSizeBytes / 1024 / 1024} MB.");

            RuleFor(x => x.Request.File.FileName)
                .Must(name => AllowedExtensions.Contains(Path.GetExtension(name).ToLowerInvariant()))
                .WithMessage("Only JPG, PNG o WEBP files are allowed.");

            RuleFor(x => x.Request.File.ContentType)
                .Must(ct => AllowedContentTypes.Contains(ct.ToLowerInvariant()))
                .WithMessage("Kind of content not allowed.");

            RuleFor(x => x.Request.File)
                .MustAsync(IsValidImageAsync)
                .WithMessage("File is not an image or is corrupted.");
        });
    }

    private static async Task<bool> IsValidImageAsync(IFormFile file, CancellationToken ct)
    {
        try
        {
            await using var stream = file.OpenReadStream();
            var imageInfo = await Image.IdentifyAsync(stream, ct);

            if (imageInfo is null) return false;

            return imageInfo.Width > 0
                && imageInfo.Height > 0
                && imageInfo.Width <= MaxDimensionPx
                && imageInfo.Height <= MaxDimensionPx;
        }
        catch
        {
            return false;
        }
    }
}