using Faradars.Application.DTOs.Users.Information.UserService;
using Faradars.Shared.Result;
using Microsoft.AspNetCore.Http;

namespace Faradars.Application.Interfaces.Services.Users.Information;

public interface IUserService
{
    // Add user = Register in Auth
    Task<Result<UserDto>> GetUserByIdAsync(int userId, CancellationToken ct);
    Task<Result<UserDto>> GetCurrentUserAsync(CancellationToken ct);
    Task<Result<List<UserDto>>> GetAllUsersInfoAsync(CancellationToken ct);
    Task<Result<List<UserDto>>> GetAllUsersByNameSearchAsync(string searchText, CancellationToken ct);
    Task<Result<List<UserDto>>> GetAllStudentsOfCourseAsync(int courseId, CancellationToken ct);
    Task<Result<UserDto>> UpdateUserInfoAsync(UpdateUserInfoDto dto, CancellationToken ct);
    Task<Result<UserDto>> DeleteUserInfoAsync(int userId, CancellationToken ct);

    Task<Result<UserDto>> AddUserProfileAsync(AddUserProfileDto dto, CancellationToken ct);
    Task<Result<UserDto>> RemoveUserProfileAsync(int userId, CancellationToken ct);
    
}