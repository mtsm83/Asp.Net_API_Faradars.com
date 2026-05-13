using Faradars.Application.DTOs.Students.Interest.InterestService;
using Faradars.Domain.Entities.Students.Interest;

namespace Faradars.Application.Mappers.Students.Interest;

public static class InterestServiceMappers
{
    public static InterestItem MapAddInterestItemDto(this AddInterestItemDto dto)
    {
        return new InterestItem
        {
            Name = dto.Name,
            Description = dto.Description
        };
    }

    public static InterestItemDto MapToInterestItemDto(this InterestItem entity)
    {
        return new InterestItemDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description
        };
    }

    public static WishlistItem MapAddWishListItemDto(this AddItemToWishListDto dto)
    {
        return new WishlistItem
        {
            UserId = dto.UserId,
            CourseId = dto.CourseId
        };
    }

    public static WishListItemDto MapToWishListItemDto(this WishlistItem entity)
    {
        return new WishListItemDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            CourseId = entity.CourseId
        };
    }
}