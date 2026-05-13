using Faradars.Application.DTOs.Courses.Pricing.CoursePriceService;
using Faradars.Domain.Entities.Courses.Pricing.CoursePrice;
using Faradars.Domain.Enums;

namespace Faradars.Application.Mappers.Courses.Pricing;

public static class CoursePriceServiceMappers
{
    public static CoursePriceDto MapToCoursePriceDto(this CoursePrice price)
    {
        return new CoursePriceDto
        {
            CourseId = price.CourseId,
            OriginalPrice = price.OriginalPrice,
            DiscountValue = price.DiscountValue,
            DiscountType = price.DiscountType,
            CurrentPrice = price.CurrentPrice,
            CreatedAt = price.CreatedAt,
            CreatorId = price.CreatedBy,
            UpdatedAt = price.UpdatedAt,
            UpdaterId = price.UpdatedBy,
            DeletedAt = price.DeletedAt,
            DeleterId = price.DeletedBy,
        };
    }

    public static CoursePrice MapAddPriceDto(this AddPriceDto dto)
    {
        return new CoursePrice
        {
            CourseId = dto.CourseId,
            OriginalPrice = dto.OriginalPrice,
            DiscountValue = dto.DiscountValue,
            DiscountType = dto.DiscountType,
            CurrentPrice = CalculateCurrentPrice(dto.OriginalPrice, dto.DiscountValue, dto.DiscountType)
        };
    }

    public static void MapUpdatePriceDto(this CoursePrice price,  UpdatePriceDto dto)
    {
        price.OriginalPrice = dto.OriginalPrice;
        price.DiscountValue = dto.DiscountValue;
        price.DiscountType = dto.DiscountType;
        price.CurrentPrice = CalculateCurrentPrice(dto.OriginalPrice, dto.DiscountValue, dto.DiscountType);
    }

    # region

    private static decimal CalculateCurrentPrice(decimal originalPrice, decimal? discountValue,
        DiscountType? discountType)
    {
        if (!discountValue.HasValue || !discountType.HasValue)
            return originalPrice;
        if (discountType.Value == DiscountType.Percentage)
            return originalPrice - originalPrice * discountValue.Value;
        if (discountType.Value == DiscountType.FixedPrice)
            return discountValue.Value;
        if (discountType.Value == DiscountType.FixedAmount)
            return originalPrice - discountValue.Value;
        return originalPrice;
    }

    # endregion
}