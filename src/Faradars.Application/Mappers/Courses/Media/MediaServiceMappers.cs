using Faradars.Application.DTOs.Courses.Media.MediaService;
using Faradars.Domain.Entities.Courses.Media;

namespace Faradars.Application.Mappers.Courses.Media;

public static class MediaServiceMappers
{
    public static AssetFileDto MapToAssetFileDto(this AssetFile entity)
    {
        return new AssetFileDto
        {
            Id = entity.Id,
            MediaType = entity.MediaType.ToString(),
            ContentType = entity.ContentType.ToString(),
            OriginalFileName = entity.OriginalFileName,
            StoredFileName = entity.StoredFileName,
            Size = entity.Size,
            Path = entity.Path,
            Description = entity.Description,
            Resolution = entity.Resolution,
            Duration = entity.Duration,
            CreatorId = entity.CreatedBy,
            UpdaterId = entity.UpdatedBy,
            DeleterId = entity.DeletedBy,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            DeletedAt = entity.DeletedAt
        };
    }
}