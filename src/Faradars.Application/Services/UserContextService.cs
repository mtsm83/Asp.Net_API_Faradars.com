using System.Security.Claims;
using Faradars.Application.DTOs;
using Faradars.Application.Interfaces.General;
using Faradars.Shared.Extensions;
using Microsoft.AspNetCore.Http;

namespace Faradars.Application.Services;

public class UserContextService : IUserContextService, IScopedDependency
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Lazy<CurrentUserDto> _currentUser;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _currentUser = new Lazy<CurrentUserDto>(BuildCurrentUser);
    }

    public CurrentUserDto CurrentUser => _currentUser.Value;
    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    #region Private Methods

    private CurrentUserDto BuildCurrentUser()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user is null || !user.Identity.IsAuthenticated)
            throw new UnauthorizedAccessException("User is not authenticated," +
                                                  "This message is from UserContextService in Application ");

        var claims = user.Claims.ToList();

        var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value.ToInt()
                     ?? throw new UnauthorizedAccessException("UserId claim is missing");
        // var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? string.Empty;

        return new CurrentUserDto
        {
            UserId = userId
        };
    }

    #endregion
}