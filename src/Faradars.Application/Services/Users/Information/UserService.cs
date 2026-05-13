using Faradars.Application.DTOs.Users.Information.UserService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Courses.Media;
using Faradars.Application.Interfaces.Services.Users.Information;
using Faradars.Application.Mappers.Users.Information;
using Faradars.Domain.Entities.Students.Enrollment;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Shared.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Users.Information;

public class UserService(
    IRepository<User> userRepository,
    IRepository<Enrollment> enrollmentRepository,
    IRepository<UserProfile> userProfileRepository,
    IMediaService mediaService,
    IUserContextService userContextService) : IUserService, IScopedDependency
{
    // Todo : profile picture setup
    public async Task<Result<UserDto>> GetUserByIdAsync(int userId, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(ct, userId);
        if (user is null)
            return Result.Failure<UserDto>(Error.NotFound);

        var userDto = user.MapToUserDto();
        return Result.Success(userDto);
    }

    public async Task<Result<UserDto>> GetCurrentUserAsync(CancellationToken ct)
    {
        var userId = userContextService.CurrentUser.UserId;
        var user = await userRepository.GetByIdAsync(ct, userId);
        if (user is null)
            return Result.Failure<UserDto>(Error.NotFound);

        var userDto = user.MapToUserDto();
        return Result.Success(userDto); 
    }

    public async Task<Result<List<UserDto>>> GetAllUsersInfoAsync(CancellationToken ct)
    {
        var users = await userRepository.TableNoTracking.ToListAsync(ct);
        if (users.Count is 0)
            return Result.Failure<List<UserDto>>(Error.NotFound);
        var userDtos = users.Select(user => user.MapToUserDto()).ToList();
        return Result.Success(userDtos);
    }

    public async Task<Result<List<UserDto>>> GetAllUsersByNameSearchAsync(string searchText, CancellationToken ct)
    {
        var users = await userRepository.TableNoTracking
            .Where(u => u.FirstName.Contains(searchText)).ToListAsync(ct);
        if (users.Count is 0)
            return Result.Failure<List<UserDto>>(Error.NotFound);

        var userDtos = users.Select(user => user.MapToUserDto()).ToList();
        return Result.Success(userDtos);
    }

    public async Task<Result<List<UserDto>>> GetAllStudentsOfCourseAsync(int courseId, CancellationToken ct)
    {
        var users = await enrollmentRepository.TableNoTracking
            .Where(en => en.CourseId == courseId)
            .Include(en => en.Student)
            .Select(en => en.Student).ToListAsync(ct);

        if (users.Count is 0)
            return Result.Failure<List<UserDto>>(Error.NotFound);

        var userDtos = users.Select(user => user.MapToUserDto()).ToList();
        return Result.Success(userDtos);
    }

    public async Task<Result<UserDto>> UpdateUserInfoAsync(UpdateUserInfoDto dto, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(ct, dto.UserId);
        if (user is null)
            return Result.Failure<UserDto>(Error.NotFound);

        user.MapUserUpdateDto(dto);
        await userRepository.UpdateAsync(user, ct);
        var userDto = user.MapToUserDto();
        return Result.Success(userDto);
    }

    public async Task<Result<UserDto>> DeleteUserInfoAsync(int userId, CancellationToken ct)
    {
        // todo: what happens to user derivations after deletion
        var user = await userRepository.GetByIdAsync(ct, userId);
        if (user is null)
            return Result.Failure<UserDto>(Error.NotFound);
        
        await userRepository.DeleteAsync(user, ct);
        var userDto = user.MapToUserDto();
        return Result.Success(userDto);
    }

    public async Task<Result<UserDto>> AddUserProfileAsync(AddUserProfileDto dto, CancellationToken ct)
    {
        var profileFile = await mediaService.UploadAssetFileAsync(dto.File, ct);
        if (profileFile.IsFailure)
            return Result.Failure<UserDto>(profileFile.Error);
        var user = await userRepository.GetByIdAsync(ct, dto.UserId);
        if (user is null)
            return Result.Failure<UserDto>(Error.NotFound);

        var newUserProfile = new UserProfile
        {
            UserId = dto.UserId,
            ProfileImageId = profileFile.Value.Id,
        };

        await userProfileRepository.AddAsync(newUserProfile, ct);
        var userDto = user.MapToUserDto();
        return Result.Success(userDto);
    }

    public async Task<Result<UserDto>> RemoveUserProfileAsync(int userId, CancellationToken ct)
    {
        var userProfile = await userProfileRepository
            .TableNoTracking
            .Where(c => c.UserId == userId)
            .Include(up => up.User)
            .FirstOrDefaultAsync(ct);
        if (userProfile is null)
            return Result.Failure<UserDto>(Error.NotFound);
        await userProfileRepository.DeleteAsync(userProfile, ct);
        var fileDeletionResult = await mediaService.DeleteAssetFileAsync(userProfile.ProfileImageId, ct);
        if (fileDeletionResult.IsFailure)
            return Result.Failure<UserDto>(fileDeletionResult.Error);
        var userDto = userProfile.User.MapToUserDto();
        return Result.Success(userDto);
    }
}