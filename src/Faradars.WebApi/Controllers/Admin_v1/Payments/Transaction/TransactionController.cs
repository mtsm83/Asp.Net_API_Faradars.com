using Asp.Versioning;
using Faradars.Application.DTOs.Payments.Transaction.TransactionService;
using Faradars.Application.Interfaces.Services.Payments.Transaction;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Payments.Transaction;

[ApiVersion("1")]
public class TransactionController(ITransactionService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> AddTransactionAsync([FromBody] AddTransactionDto dto, CancellationToken ct)
    {
        var result = await service.AddTransactionAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTransactionAsync([FromBody] UpdateTransactionDto dto, CancellationToken ct)
    {
        var result = await service.UpdateTransactionAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{transactionId:int}")]
    public async Task<IActionResult> DeleteTransactionAsync(int transactionId, CancellationToken ct)
    {
        var result = await service.DeleteTransactionAsync(transactionId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        // Assuming Unit is an empty struct or similar, you might return NoContent() or Ok()
        // depending on your API design for successful deletion.
        return Ok(result.Value); 
    }

    [HttpGet("{transactionId:int}")]
    public async Task<IActionResult> GetTransactionByIdAsync(int transactionId, CancellationToken ct)
    {
        var result = await service.GetTransactionByIdAsync(transactionId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTransactionsAsync(CancellationToken ct)
    {
        var result = await service.GetAllTransactionsAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
