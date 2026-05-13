using Faradars.Application.DTOs.Courses.Pricing.DiscountService;
using Faradars.Domain.Entities.Courses.Pricing.Discount;

namespace Faradars.Application.Mappers.Courses.Pricing;

public static class DiscountServiceMappers
{
    public static Coupon MapAddCouponDto(this AddCouponDto dto)
    {
        return new Coupon
        {
            Code = dto.Code,
            Description = dto.Description,
            DiscountValue = dto.DiscountValue,
            DiscountType = dto.DiscountType,
            MaximumDiscountAmount = dto.MaximumDiscountAmount,
            MinimumPurchase = dto.MinimumPurchase,
            ValidFrom = dto.ValidFrom,
            ValidTo = dto.ValidTo,
            UsageLimit = dto.UsageLimit,
            UsageCount = 0
        };
    }

    public static CouponDto MapToCouponDto(this Coupon coupon)
    {
        return new CouponDto
        {
            CouponId = coupon.Id,
            Code = coupon.Code,
            Description = coupon.Description,
            DiscountValue = coupon.DiscountValue,
            DiscountType = coupon.DiscountType,
            MaximumDiscountAmount = coupon.MaximumDiscountAmount,
            MinimumPurchase = coupon.MinimumPurchase,
            ValidFrom = coupon.ValidFrom,
            ValidTo = coupon.ValidTo,
            UsageLimit = coupon.UsageLimit,
            UsageCount = coupon.UsageCount
        };
    }

    public static void MapUpdateCouponDto(this Coupon coupon, UpdateCouponDto dto)
    {
        coupon.Code = dto.Code;
        coupon.Description = dto.Description;
        coupon.DiscountValue = dto.DiscountValue;
        coupon.DiscountType = dto.DiscountType;
        coupon.MaximumDiscountAmount = dto.MaximumDiscountAmount;
        coupon.MinimumPurchase = dto.MinimumPurchase;
        coupon.ValidFrom = dto.ValidFrom;
        coupon.ValidTo = dto.ValidTo;
        coupon.UsageLimit = dto.UsageLimit;
    }
    public static CouponUsage MapAddCouponUsage(this AddCouponUsageDto dto)
    {
        return new CouponUsage
        {
            CouponId = dto.CouponId,
            UserId = dto.UserId,
            OrderId = dto.OrderId,
            UsedAt = DateTime.UtcNow,
            IpAddress = dto.IpAddress
        };
    }
    public static CouponUsageDto MapToUsageDto(this CouponUsage usage)
    {
        return new CouponUsageDto
        {
            CouponUsageId = usage.Id,
            CouponId = usage.CouponId,
            UserId = usage.UserId,
            OrderId = usage.OrderId,
            UsedAt = usage.CreatedAt,
            IpAddress = usage.IpAddress
        };
    }
}