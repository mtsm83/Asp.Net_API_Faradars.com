using Faradars.Application.DTOs.Users.Address.AddressService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Users.Address;
using Faradars.Application.Mappers.Users.Address;
using Faradars.Domain.Entities.Users.Address;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Shared.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Users.Address;

public class AddressService(
    IRepository<User> userRepository,
    IRepository<Province> provinceRepository,
    IRepository<City> cityRepository,
    IRepository<UserAddress> addressRepository,
    IUserContextService userContextService
) : IAddressService, IScopedDependency
{
    public async Task<Result<ProvinceDto>> AddProvinceAsync(AddProvinceDto dto, CancellationToken ct)
    {
        var existingResource = await provinceRepository.Table
            .FirstOrDefaultAsync(p => p.Name.ToLower() == dto.Name.ToLower(), ct);
        if (existingResource != null)
            return Result.Failure<ProvinceDto>(Error.AlreadyExists);

        var userId = userContextService.CurrentUser.UserId;
        var newProvince = new Province
        {
            Name = dto.Name,
            CreatedBy = userId
        };
        await provinceRepository.AddAsync(newProvince, ct);
        var provinceDto = new ProvinceDto
        {
            Id = newProvince.Id,
            Name = newProvince.Name,
            CreatedBy = newProvince.CreatedBy,
            CreatedAt = newProvince.CreatedAt,
            UpdatedAt = newProvince.UpdatedAt,
            UpdatedBy = newProvince.UpdatedBy,
            DeletedAt = newProvince.DeletedAt,
            DeletedBy = newProvince.DeletedBy
        };
        return Result.Success(provinceDto);
    }

    public async Task<Result<Unit>> DeleteProvinceAsync(int provinceId, CancellationToken ct)
    {
        var province = await provinceRepository.GetByIdAsync(ct, provinceId);
        if (province == null)
            return Result.Failure<Unit>(Error.NotFound);

        province.DeletedAt = DateTime.Now;
        province.DeletedBy = userContextService.CurrentUser.UserId;
        await provinceRepository.UpdateAsync(province, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<ProvinceDto>> GetProvinceByIdAsync(int provinceId, CancellationToken ct)
    {
        var p = await provinceRepository.GetByIdAsync(ct, provinceId);
        if (p == null)
            return Result.Failure<ProvinceDto>(Error.NotFound);

        var provinceDto = new ProvinceDto
        {
            Id = p.Id,
            Name = p.Name,
            CreatedBy = p.CreatedBy,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt,
            UpdatedBy = p.UpdatedBy,
            DeletedAt = p.DeletedAt,
            DeletedBy = p.DeletedBy
        };
        return Result.Success(provinceDto);
    }

    public async Task<Result<List<ProvinceDto>>> GetAllProvincesAsync(CancellationToken ct)
    {
        var provinces = await provinceRepository.TableNoTracking
            .ToListAsync(ct);

        if (provinces.Count == 0)
            return Result.Failure<List<ProvinceDto>>(Error.NotFound);

        var provinceDtos = provinces.Select(p => new ProvinceDto
        {
            Id = p.Id,
            Name = p.Name,
            CreatedBy = p.CreatedBy,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt,
            UpdatedBy = p.UpdatedBy,
            DeletedAt = p.DeletedAt,
            DeletedBy = p.DeletedBy
        }).ToList();
        return Result.Success(provinceDtos);
    }

    public async Task<Result<CityDto>> AddCityAsync(AddCityDto dto, CancellationToken ct)
    {
        var province = await provinceRepository.GetByIdAsync(ct, dto.ProvinceId);
        if (province == null)
            return Result.Failure<CityDto>(Error.NotFound);

        var newCity = new City
        {
            Name = dto.Name,
            ProvinceId = dto.ProvinceId,
            CreatedBy = 0
        };
        await cityRepository.AddAsync(newCity, ct);
        var cityDto = new CityDto
        {
            Id = newCity.Id,
            Name = newCity.Name,
            ProvinceId = newCity.ProvinceId,
            CreatorId = newCity.CreatedBy,
            CreatedAt = newCity.CreatedAt,
        };
        return Result.Success(cityDto);
    }

    public async Task<Result<Unit>> DeleteCityAsync(int cityId, CancellationToken ct)
    {
        var city = await cityRepository.GetByIdAsync(ct, cityId);
        if (city == null)
            return Result.Failure<Unit>(Error.NotFound);

        await cityRepository.DeleteAsync(city, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<CityDto>> GetCityByIdAsync(int cityId, CancellationToken ct)
    {
        var city = await cityRepository.GetByIdAsync(ct, cityId);
        if (city == null)
            return Result.Failure<CityDto>(Error.NotFound);

        var cityDto = new CityDto
        {
            Id = city.Id,
            ProvinceId = city.ProvinceId,
            CreatorId = city.CreatedBy,
            Name = city.Name,
            CreatedAt = city.CreatedAt,
        };
        return Result.Success(cityDto);
    }

    public async Task<Result<List<CityDto>>> GetAllCitiesAsync(CancellationToken ct)
    {
        var cities = await cityRepository.TableNoTracking
            .ToListAsync(ct);

        if (cities.Count == 0)
            return Result.Failure<List<CityDto>>(Error.NotFound);

        var citiesDtos = cities.Select(p => new CityDto
        {
            Id = p.Id,
            Name = p.Name,
            ProvinceId = p.ProvinceId,
            CreatorId = p.CreatedBy,
            CreatedAt = p.CreatedAt,
        }).ToList();
        return Result.Success(citiesDtos);
    }

    public async Task<Result<UserAddressDto>> AddUserAddressAsync(AddUserAddressDto dto, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(ct, dto.UserId);
        if (user == null)
            return Result.Failure<UserAddressDto>(Error.NotFound);

        var city = await cityRepository.GetByIdAsync(ct, dto.CityId);
        if (city == null)
            return Result.Failure<UserAddressDto>(Error.NotFound);

        var userAddress = new UserAddress();
        userAddress.MapAddAddressDto(dto);
        userAddress.CreatedBy = userContextService.CurrentUser.UserId;

        await addressRepository.AddAsync(userAddress, ct);
        var userAddressDto = userAddress.MaptoUserAddressDto();
        return Result.Success(userAddressDto);
    }

    public async Task<Result<UserAddressDto>> UpdateUserAddressAsync(UpdateUserAddressDto dto, CancellationToken ct)
    {
        var address = await addressRepository.GetByIdAsync(ct, dto.AddressId);
        if (address == null)
            return Result.Failure<UserAddressDto>(Error.NotFound);

        address.MapUpdateAddressDto(dto);
        address.UpdatedBy = 0;
        address.UpdatedAt = DateTime.Now;
        await addressRepository.UpdateAsync(address, ct);
        var userAddressDto = address.MaptoUserAddressDto();
        return Result.Success(userAddressDto);
    }

    public async Task<Result<Unit>> DeleteUserAddressAsync(int userId, CancellationToken ct)
    {
        var address = await addressRepository.TableNoTracking
            .FirstOrDefaultAsync(x => x.UserId == userId, ct);
        if (address == null)
            return Result.Failure<Unit>(Error.NotFound);
        await addressRepository.DeleteAsync(address, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<UserAddressDto>> GetUserAddressAsync(int userId, CancellationToken ct)
    {
        var address = await addressRepository.TableNoTracking
            .FirstOrDefaultAsync(x => x.UserId == userId, ct);
        if (address == null)
            return Result.Failure<UserAddressDto>(Error.NotFound);
        var addressDto = address.MaptoUserAddressDto();
        return Result.Success(addressDto);
    }
}