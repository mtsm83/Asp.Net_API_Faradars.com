using Faradars.Application.DTOs.Courses.Pricing.BundleService;
using Faradars.Domain.Entities.Courses.Pricing.Bundle;

namespace Faradars.Application.Mappers.Courses.Pricing;

public static class BundleServiceMappers
{
    public static Bundle MapAddDtoToBundle(this AddBundleDto dto)
    {
        return new Bundle
        {
            Title = dto.Title,
            Description = dto.Description,
        };
    }

    public static void MapUpdateDtoToBundle(this Bundle entity, UpdateBundleDto dto)
    {
        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.DiscountValue = dto.DiscountValue;
        entity.DiscountType = dto.DiscountType;
        entity.ValidFrom = dto.ValidFrom;
        entity.ValidTo = dto.ValidTo;
        entity.BannerImageId = dto.BannerImageId ?? entity.BannerImageId;
    }

    public static BundleDto MapToBundleDto(this Bundle entity)
    {
        return new BundleDto
        {
            BundleId = entity.Id,
            Title = entity.Title,
            BannerImageId = entity.BannerImageId,
            Description = entity.Description,
            DiscountValue = entity.DiscountValue,
            DiscountType = entity.DiscountType,
            ValidFrom = entity.ValidFrom,
            ValidTo = entity.ValidTo,
            CreatorId = entity.CreatedBy,
            UpdaterId = entity.UpdatedBy,
            DeleterId = entity.DeletedBy,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            DeletedAt = entity.DeletedAt
        };
    }

    public static BundleCourseDto MapToBundleCourseDto(this BundleCourse entity)
    {
        return new BundleCourseDto
        {
            Id = entity.Id,
            BundleId = entity.BundleId,
            CourseId = entity.CourseId,
            CourseTitle = entity.Course?.Title,
            DisplayOrder = entity.DisplayOrder,
            AddedAt = entity.CreatedAt
        };
    }
}