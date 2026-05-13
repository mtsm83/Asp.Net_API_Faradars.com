using Faradars.Application.DTOs.Users.Address.AddressService;
using Faradars.Domain.Entities.Users.Address;

namespace Faradars.Application.Mappers.Users.Address;

public static class AddressServiceMappers
{
    public static void MapAddAddressDto(this UserAddress address, AddUserAddressDto dto)
    {
        address.UserId = dto.UserId;
        address.CityId = dto.CityId;
        address.PostalCode = dto.PostalCode;
        address.FullAddress = dto.FullAddress;
    }

    public static void MapUpdateAddressDto(this UserAddress address, UpdateUserAddressDto dto)
    {
        address.CityId = dto.CityId;
        address.PostalCode = dto.PostalCode;
        address.FullAddress = dto.FullAddress;
    }

    public static UserAddressDto MaptoUserAddressDto(this UserAddress address)
    {
        return new UserAddressDto
        {
            Id = address.Id,
            UserId = address.UserId,
            CityId = address.CityId,
            PostalCode = address.PostalCode,
            FullAddress = address.FullAddress,
            CreatorId = address.CreatedBy,
            CreatedAt = address.CreatedAt
        };
    }
}