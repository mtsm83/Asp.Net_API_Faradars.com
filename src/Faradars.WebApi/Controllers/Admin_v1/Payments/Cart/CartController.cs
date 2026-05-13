using Asp.Versioning;
using Faradars.Application.DTOs.Payments.Cart.CartService;
using Faradars.Application.Interfaces.Services.Payments.Cart;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Payments.Cart;

[ApiVersion("1")]
public class CartController(ICartService service) : AdminBaseController
{
    [HttpPost("user/{userId:int}")]
    public async Task<IActionResult> AddCartForUserAsync(int userId, CancellationToken ct)
    {
        var result = await service.AddCartForUserAsync(userId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("user/{userId:int}")]
    public async Task<IActionResult> DeleteCartForUserAsync(int userId, CancellationToken ct)
    {
        var result = await service.DeleteCartForUserAsync(userId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPost("item")]
    public async Task<IActionResult> AddItemToCartAsync([FromBody] AddToCartDto dto, CancellationToken ct)
    {
        var result = await service.AddItemToCartAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("item")]
    public async Task<IActionResult> RemoveFromCartAsync([FromBody] RemoveFromCartDto dto, CancellationToken ct)
    {
        var result = await service.RemoveFromCartAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetUserCartAsync(int userId, CancellationToken ct)
    {
        var result = await service.GetUserCartAsync(userId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
