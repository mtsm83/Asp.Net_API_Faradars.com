using Asp.Versioning;
using Faradars.Application.DTOs.Users.Address.AddressService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Users.Address;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.User_v1.Users.Address;

[ApiVersion("1")]
public class AddressController(
    IAddressService service
    ) : AdminBaseController
{

    [HttpPost("province")]
    public async Task<IActionResult> AddProvinceAsync([FromBody] AddProvinceDto dto, CancellationToken ct)
    {
        // var userId = userContextService.CurrentUser.UserId;
        var result = await service.AddProvinceAsync(dto, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpDelete("province/{provinceId:int}")]
    public async Task<IActionResult> DeleteProvinceAsync(int provinceId, CancellationToken ct)
    {
        var result = await service.DeleteProvinceAsync(provinceId, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok();
    }

    [HttpGet("province/{provinceId:int}")]
    public async Task<IActionResult> GetProvinceByIdAsync(int provinceId, CancellationToken ct)
    {
        var result = await service.GetProvinceByIdAsync(provinceId, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpGet("provinces")]
    public async Task<IActionResult> GetAllProvincesAsync(CancellationToken ct)
    {
        var result = await service.GetAllProvincesAsync(ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    // --- City Operations ---

    [HttpPost("city")]
    public async Task<IActionResult> AddCityAsync([FromBody] AddCityDto dto, CancellationToken ct)
    {
        var result = await service.AddCityAsync(dto, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpDelete("city/{cityId:int}")]
    public async Task<IActionResult> DeleteCityAsync(int cityId, CancellationToken ct)
    {
        var result = await service.DeleteCityAsync(cityId, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok();
    }

    [HttpGet("city/{cityId:int}")]
    public async Task<IActionResult> GetCityByIdAsync(int cityId, CancellationToken ct)
    {
        var result = await service.GetCityByIdAsync(cityId, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpGet("cities")]
    public async Task<IActionResult> GetAllCitiesAsync(CancellationToken ct)
    {
        var result = await service.GetAllCitiesAsync(ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    // --- User Address Operations ---

    [HttpPost("user-address")]
    public async Task<IActionResult> AddUserAddressAsync([FromBody] AddUserAddressDto dto, CancellationToken ct)
    {
        var result = await service.AddUserAddressAsync(dto, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpPut("user-address")]
    public async Task<IActionResult> UpdateUserAddressAsync([FromBody] UpdateUserAddressDto dto, CancellationToken ct)
    {
        var result = await service.UpdateUserAddressAsync(dto, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpDelete("user-address/{userId:int}")]
    public async Task<IActionResult> DeleteUserAddressAsync(int userId, CancellationToken ct)
    {
        var result = await service.DeleteUserAddressAsync(userId, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok();
    }

    [HttpGet("user-address/{userId:int}")]
    public async Task<IActionResult> GetUserAddressAsync(int userId, CancellationToken ct)
    {
        var result = await service.GetUserAddressAsync(userId, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }
}
