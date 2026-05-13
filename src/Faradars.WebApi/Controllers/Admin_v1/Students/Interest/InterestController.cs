using Asp.Versioning;
using Faradars.Application.DTOs.Students.Interest.InterestService;
using Faradars.Application.Interfaces.Services.Students.Interest;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Students.Interest;

[ApiVersion("1")]
public class InterestController(IInterestService service) : AdminBaseController
{
    // ------------------- Interest Items -------------------

    [HttpPost("item")]
    public async Task<IActionResult> AddInterestItemAsync([FromBody] AddInterestItemDto dto, CancellationToken ct)
    {
        var result = await service.AddInterestItemAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("item")]
    public async Task<IActionResult> DeleteInterestItemAsync([FromBody] DeleteInterestItemDto dto, CancellationToken ct)
    {
        var result = await service.DeleteInterestItemAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("item/{interestId:int}")]
    public async Task<IActionResult> GetInterestItemByIdAsync(int interestId, CancellationToken ct)
    {
        var result = await service.GetInterestItemByIdAsync(interestId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("items")]
    public async Task<IActionResult> GetAllInterestItemsAsync(CancellationToken ct)
    {
        var result = await service.GetAllInterestItemsAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    // ------------------- Wish List -------------------

    [HttpPost("wishlist")]
    public async Task<IActionResult> AddItemToWishListAsync([FromBody] AddItemToWishListDto dto, CancellationToken ct)
    {
        var result = await service.AddItemToWishListAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("wishlist")]
    public async Task<IActionResult> DeleteItemFromWishListAsync([FromBody] DeleteItemThanWishListDto dto, CancellationToken ct)
    {
        var result = await service.DeleteItemToWishListAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("wishlist/{userId:int}")]
    public async Task<IActionResult> GetUserWishListAsync(int userId, CancellationToken ct)
    {
        var result = await service.GetUserWishListAsync(userId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
