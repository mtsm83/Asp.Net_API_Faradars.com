using Faradars.Application.DTOs;

namespace Faradars.Application.Interfaces.General;

public interface IUserContextService
{
    public CurrentUserDto CurrentUser { get; }
    bool IsAuthenticated { get; }
}