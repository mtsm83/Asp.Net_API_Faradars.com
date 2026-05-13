using System.Security.Claims;
using Faradars.Application.DTOs.Auth;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Domain.Entities.Users.Role;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.General;

public interface IJwtTokenManager
{
    AccessTokenDto GenerateAccessToken(User user,Role role);
    Result<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
}