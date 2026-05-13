using Asp.Versioning;
using Faradars.Application.Interfaces.Services.Payments.Order;
using Faradars.Domain.Enums;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Payments.Order;

[ApiVersion("1")]
public class OrderController(IOrderService service) : AdminBaseController
{
    [HttpPost("user/{userId:int}/fromCart")]
    public async Task<IActionResult> AddOrderFromCartAsync(int userId, CancellationToken ct)
    {
        var result = await service.AddOrderFromCartAsync(userId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{orderId:int}")]
    public async Task<IActionResult> GetOrderByIdAsync(int orderId, CancellationToken ct)
    {
        var result = await service.GetOrderByIdAsync(orderId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetUserOrdersAsync(int userId, CancellationToken ct)
    {
        var result = await service.GetUserOrdersAsync(userId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut("{orderId:int}/confirm")]
    public async Task<IActionResult> ConfirmOrderAsync(int orderId, CancellationToken ct)
    {
        var result = await service.ConfirmOrderAsync(orderId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{orderId:int}")]
    public async Task<IActionResult> DeleteOrderAsync(int orderId, CancellationToken ct)
    {
        var result = await service.DeleteOrderAsync(orderId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrdersAsync(CancellationToken ct)
    {
        var result = await service.GetAllOrdersAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut("{orderId:int}/status")]
    public async Task<IActionResult> UpdateOrderStatusAsync(int orderId, [FromBody] OrderStatus status, CancellationToken ct)
    {
        var result = await service.UpdateOrderStatusAsync(orderId, status, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("byDateRange")]
    public async Task<IActionResult> GetOrdersByDateRangeAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, CancellationToken ct)
    {
        var result = await service.GetOrdersByDateRangeAsync(startDate, endDate, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
