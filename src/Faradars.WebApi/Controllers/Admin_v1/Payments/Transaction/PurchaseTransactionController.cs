using Asp.Versioning;
using Faradars.Application.DTOs.Payments.Transaction.PurchaseTransactionService;
using Faradars.Application.Interfaces.Services.Payments.Transaction;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Payments.Transaction;

[ApiVersion("1")]
public class PurchaseTransactionController(IPurchaseTransactionService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> AddPurchaseTransactionAsync([FromBody] AddPurchaseTransactionDto dto, CancellationToken ct)
    {
        var result = await service.AddPurchaseTransactionAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePurchaseTransactionAsync([FromBody] UpdatePurchaseTransactionDto dto, CancellationToken ct)
    {
        var result = await service.UpdatePurchaseTransactionAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{transactionId:int}")]
    public async Task<IActionResult> DeletePurchaseTransactionAsync(int transactionId, CancellationToken ct)
    {
        var result = await service.DeletePurchaseTransactionAsync(transactionId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{transactionId:int}")]
    public async Task<IActionResult> GetPurchaseTransactionByIdAsync(int transactionId, CancellationToken ct)
    {
        var result = await service.GetPurchaseTransactionByIdAsync(transactionId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPurchaseTransactionsAsync(CancellationToken ct)
    {
        var result = await service.GetAllPurchaseTransactionsAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
