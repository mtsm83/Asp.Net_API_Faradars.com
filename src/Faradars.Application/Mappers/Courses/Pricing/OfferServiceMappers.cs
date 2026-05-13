using Faradars.Application.DTOs.Courses.Pricing.OfferService;
using Faradars.Domain.Entities.Courses.Pricing.Offer;

namespace Faradars.Application.Mappers.Courses.Pricing;

public static class OfferServiceMappers
{
    public static Offer MapAddOfferDto(this AddOfferDto dto)
    {
        return new Offer
        {
            Title = dto.Title,
            Description = dto.Description,
            DiscountValue = dto.DiscountValue,
            DiscountType = dto.DiscountType,
            Slug = dto.Slug,
            QuantityLimit = dto.QuantityLimit,
            OfferStarts = dto.OfferStarts,
            OfferEnds = dto.OfferEnds
        };
    }

    public static OfferDto MapToOfferDto(this Offer offer)
    {
        return new OfferDto
        {
            OfferId = offer.Id,
            Title = offer.Title,
            Description = offer.Description,
            DiscountValue = offer.DiscountValue,
            DiscountType = offer.DiscountType,
            Slug = offer.Slug,
            QuantityLimit = offer.QuantityLimit,
            OfferStarts = offer.OfferStarts,
            OfferEnds = offer.OfferEnds,
            BannerImageId = offer.BannerImageId,
            CreatedAt = offer.CreatedAt,
            UpdatedAt = offer.UpdatedAt,
            CreatorId = offer.CreatedBy,
            UpdaterId = offer.UpdatedBy,
            DeletedAt = offer.DeletedAt,
            DeleterId = offer.DeletedBy,
        };
    }
    public static void MapUpdateOfferDto(this Offer offer, UpdateOfferDto dto)
    {
        offer.Title = dto.Title;
        offer.Description = dto.Description;
        offer.DiscountValue = dto.DiscountValue;
        offer.DiscountType = dto.DiscountType;
        offer.Slug = dto.Slug;
        offer.QuantityLimit = dto.QuantityLimit;
        offer.OfferStarts = dto.OfferStarts;
        offer.OfferEnds = dto.OfferEnds;
    }
}