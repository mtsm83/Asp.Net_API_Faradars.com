using Faradars.Application.DTOs.Courses.Media.MediaService;
using Faradars.Shared.Result;
using Microsoft.AspNetCore.Http;

namespace Faradars.Application.Interfaces.Services.Courses.Media;

public interface IMediaService
{
    Task<Result<AssetFileDto>> UploadAssetFileAsync(IFormFile file, CancellationToken ct);
    Task<Result<AssetFileDto>> DeleteAssetFileAsync(int? fileId, CancellationToken ct);
    Task<Result<AssetFileDto>> GetAssetFileByIdAsync(int? fileId, CancellationToken ct);
    Task<Result<DownloadMediaDto>> DownloadAssetFileAsync(string fileUrl, CancellationToken ct);
}