using Asp.Versioning;
using Faradars.Application.DTOs.Auth;
using Faradars.Application.Interfaces.Services.Auth;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Auth_v1;

[ApiVersion("1")]
public class AuthController(IAuthService authService) : AuthBaseController
{
    [HttpPost("register/phone")]
    public async Task<IActionResult> RegisterByPhoneAsync([FromBody] RegisterByPhoneDto dto, CancellationToken ct)
    {
        var result = await authService.RegisterByPhoneAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Message
            });

        return Ok(result.Value);
    }

    [HttpPost("login/phone")]
    public async Task<IActionResult> LoginByPhoneAsync([FromBody] LoginByPhoneDto dto, CancellationToken ct)
    {
        var result = await authService.LoginByPhoneAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Message
            });

        return Ok(result.Value);
    }

    [HttpPost("register/email")]
    public async Task<IActionResult> RegisterByEmailAsync([FromBody] RegisterByEmailDto dto, CancellationToken ct)
    {
        var result = await authService.RegisterByEmailAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Message
            });

        return Ok(result.Value);
    }

    [HttpPost("login/email")]
    public async Task<IActionResult> LoginByEmailAsync([FromBody] LoginByEmailDto dto, CancellationToken ct)
    {
        var result = await authService.LoginByEmailAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Message
            });

        return Ok(result.Value);
    }

    [HttpPost("send-email-verification")]
    public async Task<IActionResult> SendEmailVerificationCodeAsync([FromBody] string email, CancellationToken ct)
    {
        var result = await authService.SendEmailVerificationCodeAsync(email, ct);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Message
            });

        return Ok(new { message = "Verification code sent to email successfully" });
    }

    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmailAsync([FromBody] VerifyEmailDto dto, CancellationToken ct)
    {
        var result = await authService.VerifyEmailAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Message
            });

        return Ok(result.Value);
    }

    [HttpPost("send-phone-verification")]
    public async Task<IActionResult> SendPhoneVerificationCodeAsync([FromBody] string phone, CancellationToken ct)
    {
        var result = await authService.SendPhoneVerificationCodeAsync(phone, ct);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Message
            });

        return Ok(new { message = "Verification code sent to phone successfully" });
    }

    [HttpPost("verify-phone")]
    public async Task<IActionResult> VerifyPhoneAsync([FromBody] VerifyPhoneDto dto, CancellationToken ct)
    {
        var result = await authService.VerifyPhoneAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Message
            });

        return Ok(result.Value);
    }
}