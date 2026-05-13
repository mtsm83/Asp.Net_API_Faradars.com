using Faradars.Application.DTOs.Users.Information.UserService;
using Faradars.Domain.Entities.Users.Information;

namespace Faradars.Application.Mappers.Users.Information;

public static class UserServiceMappers
{
    public static UserDto MapToUserDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            IsPhoneVerified = user.IsPhoneVerified,
            IsEmailVerified = user.IsEmailVerified,
            BirthDate = user.BirthDate,
            Gender = user.Gender?.ToString(),
            NCode = user.NCode,
            ProfileImageId = user.UserProfiles?.FirstOrDefault()?.ProfileImageId,
            ProfileImageUrl = user.UserProfiles?.FirstOrDefault()?.ProfileImage?.Path,
            RegisteredAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            UpdaterId = user.UpdatedBy,
            DeletedAt = user.DeletedAt,
            DeleterId = user.DeletedBy,
        };
    }

    public static void MapUserUpdateDto(this User user, UpdateUserInfoDto dto)
    {
        user.FirstName = dto.FirstName ?? user.FirstName;
        user.LastName = dto.LastName;
        user.BirthDate = dto.BirthDate;
        user.Gender = dto.Gender ?? user.Gender;
        user.NCode = dto.NCode;
    }
}