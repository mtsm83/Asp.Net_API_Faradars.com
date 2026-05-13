using Faradars.Application.DTOs.Users.Address.AddressService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Users.Address;

public interface IAddressService
{
    Task<Result<ProvinceDto>> AddProvinceAsync(AddProvinceDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteProvinceAsync(int provinceId, CancellationToken ct);
    Task<Result<ProvinceDto>> GetProvinceByIdAsync(int provinceId, CancellationToken ct);
    Task<Result<List<ProvinceDto>>> GetAllProvincesAsync(CancellationToken ct);

    Task<Result<CityDto>> AddCityAsync(AddCityDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteCityAsync(int cityId, CancellationToken ct);
    Task<Result<CityDto>> GetCityByIdAsync(int cityId, CancellationToken ct);
    Task<Result<List<CityDto>>> GetAllCitiesAsync(CancellationToken ct);
    
    Task<Result<UserAddressDto>> AddUserAddressAsync(AddUserAddressDto dto, CancellationToken ct);
    Task<Result<UserAddressDto>> UpdateUserAddressAsync(UpdateUserAddressDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteUserAddressAsync(int userId, CancellationToken ct);
    Task<Result<UserAddressDto>> GetUserAddressAsync(int userId, CancellationToken ct);
    
}