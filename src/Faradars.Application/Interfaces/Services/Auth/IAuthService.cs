using Faradars.Application.DTOs.Auth;
using Faradars.Shared.Result;
using UserDto = Faradars.Application.DTOs.Users.Information.UserService.UserDto;

namespace Faradars.Application.Interfaces.Services.Auth;

public interface IAuthService
{
    // todo: Implement refresh token
    Task<Result<UserDto>> RegisterByPhoneAsync(RegisterByPhoneDto dto, CancellationToken ct);
    Task<Result<UserDto>> LoginByPhoneAsync(LoginByPhoneDto byPhoneDto, CancellationToken ct); 
    Task<Result<UserDto>> RegisterByEmailAsync(RegisterByEmailDto dto, CancellationToken ct);
    Task<Result<UserDto>> LoginByEmailAsync(LoginByEmailDto byPhoneDto, CancellationToken ct);
    Task<Result<Unit>> SendEmailVerificationCodeAsync(string email, CancellationToken ct);
    Task<Result<UserDto>> VerifyEmailAsync(VerifyEmailDto dto, CancellationToken ct);
    Task<Result<Unit>> SendPhoneVerificationCodeAsync(string phone, CancellationToken ct);
    Task<Result<UserDto>> VerifyPhoneAsync(VerifyPhoneDto dto, CancellationToken ct);
}