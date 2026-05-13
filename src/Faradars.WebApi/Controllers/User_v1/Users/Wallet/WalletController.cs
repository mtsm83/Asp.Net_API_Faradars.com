using Asp.Versioning;
using Faradars.Application.DTOs.Users.Wallet.WalletService;
using Faradars.Application.Interfaces.Services.Users.Wallet;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.User_v1.Users.Wallet;

[ApiVersion("1")]
public class WalletController(IWalletService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> AddUserWalletAsync([FromBody] CreateWalletDto dto, CancellationToken ct)
    {
        var result = await service.AddUserWalletAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{walletId:int}")]
    public async Task<IActionResult> DeleteUserWalletAsync(int walletId, CancellationToken ct)

    {
        var result = await service.DeleteUserWalletAsync(walletId, ct);

        if (result.IsFailure)

            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok();
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetUserWalletAsync(int userId, CancellationToken ct)

    {
        var result = await service.GetUserWalletAsync(userId, ct);

        if (result.IsFailure)

            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllUserWalletsAsync(CancellationToken ct)
    {
        var result = await service.GetAllUserWalletsAsync(ct);
        if (result.IsFailure)

            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}