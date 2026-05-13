using Faradars.Application.DTOs.Courses.Media.MediaService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Courses.Media;
using Faradars.Application.Mappers.Courses.Media;
using Faradars.Domain.Entities.Courses.Media;
using Faradars.Domain.Enums;
using Faradars.Shared.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Courses.Media;

public class MediaService(
    IUserContextService userContextService,
    IRepository<AssetFile> assetRepository) : IMediaService, IScopedDependency
{
    public async Task<Result<AssetFileDto>> UploadAssetFileAsync(IFormFile file, CancellationToken ct)
    {
        try
        {
            // Generate unique filename
            var fileExtension = Path.GetExtension(file.FileName);
            var storedFileName = $"{Guid.NewGuid():N}{fileExtension}";

            // Define storage path (you can hard code or use a constant)
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            var filePath = Path.Combine(uploadsFolder, storedFileName);

            // Ensure directory exists
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream, ct);
            }

            // Create and save entity
            var assetFile = new AssetFile
            {
                MediaType = GetMediaTypeFromContentType(file.ContentType),
                ContentType = GetContentTypeFromExtension(fileExtension),
                OriginalFileName = file.FileName,
                StoredFileName = storedFileName,
                Size = file.Length,
                Path = $"/uploads/{storedFileName}",
                CreatedBy = userContextService.CurrentUser.UserId
            };

            await assetRepository.AddAsync(assetFile, ct);
            return Result.Success(assetFile.MapToAssetFileDto());
        }
        catch (Exception ex)
        {
            return Result.Failure<AssetFileDto>(Error.InternalServerError);
        }
    }

    public async Task<Result<AssetFileDto>> DeleteAssetFileAsync(int? fileId, CancellationToken ct)
    {
        try
        {
            var assetFile = await assetRepository.GetByIdAsync(ct, fileId);

            if (assetFile == null)
                return Result.Failure<AssetFileDto>(Error.NotFound);
            await assetRepository.DeleteAsync(assetFile, ct);

            // Optional: Delete physical file
            // var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads",
            //     assetFile.StoredFileName);
            // if (File.Exists(filePath))
            //     File.Delete(filePath);

            return Result.Success(assetFile.MapToAssetFileDto());
        }
        catch (Exception ex)
        {
            return Result.Failure<AssetFileDto>(Error.InternalServerError);
        }
    }

    public async Task<Result<AssetFileDto>> GetAssetFileByIdAsync(int? fileId, CancellationToken ct)
    {
        try
        {
            var assetFile = await assetRepository.GetByIdAsync(ct, fileId);

            if (assetFile == null)
                return Result.Failure<AssetFileDto>(Error.NotFound);

            return Result.Success(assetFile.MapToAssetFileDto());
        }
        catch (Exception ex)
        {
            return Result.Failure<AssetFileDto>(Error.InternalServerError);
        }
    }

    public async Task<Result<DownloadMediaDto>> DownloadAssetFileAsync(string fileUrl, CancellationToken ct)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(fileUrl))
                return Result.Failure<DownloadMediaDto>(Error.NotFound);

            // Extract filename from URL
            var fileName = Path.GetFileName(fileUrl);
            var storedFileName = fileName; // The URL contains the stored filename

            // Find the asset in database
            var assetFile = await assetRepository.Table
                .FirstOrDefaultAsync(a => a.StoredFileName == storedFileName, ct);

            if (assetFile == null)
                return Result.Failure<DownloadMediaDto>(Error.NotFound);

            // Build physical file path
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            var filePath = Path.Combine(uploadsFolder, assetFile.StoredFileName);

            // Check if file exists physically
            if (!File.Exists(filePath))
                return Result.Failure<DownloadMediaDto>(Error.NotFound);

            // Read file bytes
            var fileBytes = await File.ReadAllBytesAsync(filePath, ct);
            var contentType = GetMimeType(assetFile.ContentType);

            var fileDownloadDto = new DownloadMediaDto
            {
                FileName = assetFile.OriginalFileName,
                FileContent = fileBytes,
                ContentType = contentType,
                FileSize = assetFile.Size
            };

            return Result.Success(fileDownloadDto);
        }
        catch (Exception ex)
        {
            return Result.Failure<DownloadMediaDto>(Error.InternalServerError);
        }
    }

// Helper method to get MIME type

    #region Helper Methods

    private string GetMimeType(ContentType contentType)
    {
        return contentType switch
        {
            ContentType.Jpeg => "image/jpeg",
            ContentType.Png => "image/png",
            ContentType.Gif => "image/gif",
            ContentType.Mp4 => "video/mp4",
            ContentType.Mp3 => "audio/mpeg",
            ContentType.Pdf => "application/pdf",
            ContentType.Doc => "application/msword",
            _ => "application/octet-stream"
        };
    }

    private MediaType GetMediaTypeFromContentType(string contentType)
    {
        if (contentType.StartsWith("image/"))
            return MediaType.Image;
        if (contentType.StartsWith("video/"))
            return MediaType.Video;
        if (contentType.StartsWith("audio/"))
            return MediaType.Audio;

        return MediaType.Document;
    }

    private ContentType GetContentTypeFromExtension(string extension)
    {
        return extension.ToLower() switch
        {
            ".jpg" or ".jpeg" => ContentType.Jpeg,
            ".png" => ContentType.Png,
            ".gif" => ContentType.Gif,
            ".mp4" => ContentType.Mp4,
            ".mp3" => ContentType.Mp3,
            ".pdf" => ContentType.Pdf,
            ".doc" or ".docx" => ContentType.Doc,
            _ => ContentType.Other
        };
    }

    #endregion
}