using Asp.Versioning;
using Faradars.Application.DTOs.Payments.Transaction.WalletTransactionService;
using Faradars.Application.Interfaces.Services.Payments.Transaction;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Payments.Transaction;

[ApiVersion("1")]
public class WalletTransactionController(IWalletTransactionService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> AddWalletTransactionAsync([FromBody] AddWalletTransactionDto dto, CancellationToken ct)
    {
        var result = await service.AddWalletTransactionAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateWalletTransactionAsync([FromBody] UpdateWalletTransactionDto dto, CancellationToken ct)
    {
        var result = await service.UpdateWalletTransactionAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{transactionId:int}")]
    public async Task<IActionResult> DeleteWalletTransactionAsync(int transactionId, CancellationToken ct)
    {
        var result = await service.DeleteWalletTransactionAsync(transactionId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        // Assuming Unit is an empty struct or similar, you might return NoContent() or Ok()
        // depending on your API design for successful deletion.
        return Ok(result.Value);
    }

    [HttpGet("{transactionId:int}")]
    public async Task<IActionResult> GetWalletTransactionByIdAsync(int transactionId, CancellationToken ct)
    {
        var result = await service.GetWalletTransactionByIdAsync(transactionId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWalletTransactionsAsync(CancellationToken ct)
    {
        var result = await service.GetAllWalletTransactionsAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
